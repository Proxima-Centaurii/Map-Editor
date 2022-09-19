using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace Map_Editor
{
	/// <summary>
	/// This class contains all the software's functionality
	/// </summary>
	
	public abstract class Core
	{
		//Constants
		protected const float WIDTH = 620.0f , HEIGHT = 407.0f; //Represents the canvas's size
		protected const float HALF_WIDTH = WIDTH/2, HALF_HEIGHT = HEIGHT/2;
		protected const float MIN_SCALE = .25f, MAX_SCALE = 4.0f;
		
		//File
		public String fileFilter;
		
		//GUI
		protected Label statusBar;
		
		//Visual
		protected Bitmap bufferImage;
		protected Graphics gfx,g;
		protected SolidBrush sBrush;
		protected Pen pen;
		
		protected Font defaultFont;
		
		//Timing
		protected int FRAME_MILLIS;
		protected Stopwatch sWatch;
		private bool allowDraw = true;
		
		//World
		protected PointF worldOrigin;
		protected float scale = 1.0f;
		
		
		/*ABSTRACT METHODS*/
		protected abstract void draw();
		
		//Inputs
		public abstract void mouseMoved(Point mousePos, Point lastMousePos);
		public abstract void mouseDragged(Point mousePos, Point lastMousePos, MouseButtons button);
		public abstract void mouseUp(Point mousePos, MouseButtons button);
		public abstract void mouseDown(Point mousePos, MouseButtons button);

		//Editor operations
		public abstract void changedSelection(int id);
		//File operations
		public abstract void newMap();
		public abstract void loadMap(String path);
		public abstract void saveMap(String path);
		
		
		
		/*CLASS METHODS*/
		protected void initBase(ref Graphics gfx, ref SolidBrush sBrush, ref Pen pen, ref Label statusBar){
			this.gfx = gfx;
			this.sBrush = sBrush;
			this.pen = pen;
			
			bufferImage = new Bitmap((int)WIDTH,(int)HEIGHT);
			g = Graphics.FromImage(bufferImage);
			
			defaultFont = new Font("Arial", 8.25f);
			
			this.statusBar = statusBar;
			
			worldOrigin = new PointF(HALF_WIDTH,HALF_HEIGHT);
			
			sWatch = new Stopwatch();
		}
		
		//Basic resource disposing, can be overrided to dispose more objects
		//This function eliminates the refferences to the disposable objects
		public virtual void Dispose(){
			gfx = null;
			sBrush = null;
			pen = null;
			
			g.Dispose();
			bufferImage.Dispose();
			
			defaultFont.Dispose();
			
			sWatch.Reset();
			sWatch = null;
			
			statusBar = null;
		}
		
		protected void setFrameSpeed(int FPS){
			float t = 1000.0f/((float)FPS);
			FRAME_MILLIS = (int)t;
		}
		
		/*Return codes:
		 0 - drawed
		 1 - waiting to draw
		 -1 - too soon to draw
		 */
		public int requestDraw(){
			
			if(allowDraw){
				g.Clear(Color.Black);
				draw();
				allowDraw = false;
				sWatch.Restart();
				return 0;
			}
			else if(sWatch.ElapsedMilliseconds > FRAME_MILLIS){
				allowDraw = true;
				return 1;
			}
				
			return -1;
		}
	}
	
	//------------------------------------------------------------------------------------------------------------
	public class CoreFIXED : Core
	{	
		private const int OBJECT_TYPE_COUNT = 4;
		private const int BLOCK_SIZE = 20;
		private const int WORLD_WIDTH = 40;
		private const int WORLD_HEIGHT = 30;
		private const int WORLD_TABLE_SIZE = WORLD_WIDTH * WORLD_HEIGHT;
		private const String statusBarFormat = "X: {0} | Y: {1} | Scale: {2:0.00} | Selected object: {3}";
		
		//Brush
		private PointF brushPos;	//points to the upper left corner of the brush
		private Point mouseWorldPos;
		
		//Object data
		private Image NO_SYMBOL = Image.FromFile("res/null.png");
		private Image[] symbol;
		
		//World data
		private int currentObject,emptyObject; //Stores the current object that is being used.; Represents an empty object.
		private int currentObjectSymbolId = 0;
		private int[] listLink; //links the ListBox items to their corresponding world object
		private String[] objectNames;	/*Stores the names of the object found in the listbox of the user interface.
										  It's a separate field from WorldObject so it could be passed as a list to the ListBox object.	*/
		
		private List<int> tableObjects;
		private List<int> tableObjectSymbolId;
		private int tileCount = 0;
		
		private float scaledUnit = BLOCK_SIZE;
		
		public CoreFIXED(ref Graphics gfx, ref SolidBrush sBrush, ref Pen pen, ref Label statusBar, ref ListBox lb){
			initBase(ref gfx, ref sBrush, ref pen, ref statusBar);
			setFrameSpeed(60);
			fileFilter = "FIXED map(.lvl)|*.lvl";
			
			//Initializing object data
			// !! THIS ORDER MUST BE KEPT !!
			symbol = new Image[OBJECT_TYPE_COUNT];
			listLink = new int[OBJECT_TYPE_COUNT];
			objectNames = new String[OBJECT_TYPE_COUNT];
			
			symbol[0] = Image.FromFile("res/player.png");
			objectNames[0] = "Player";
			listLink[0] = 0;
			
			symbol[1] = Image.FromFile("res/wall.png");
			objectNames[1] = "Wall";
			listLink[1] = 1;
			
			symbol[2] = Image.FromFile("res/goal.png");
			objectNames[2] = "Goal";
			listLink[2] = 2;
			
			symbol[3] = Image.FromFile("res/star.png");
			objectNames[3] = "Star";
			listLink[3] = 3;
			
			
			//Initializing the object list in main form
			lb.Items.Clear();
			lb.Items.AddRange(objectNames);
			
			
			//Preparing world
			tableObjects = new List<int>();
			tableObjectSymbolId = new List<int>();
			
			emptyObject = -1;
			currentObject = listLink[0];
			brushPos = new PointF();
			
			mouseWorldPos = new Point();
			
			Console.Write("FIXED Core class initialized.\n");
			sWatch.Start();
		}
		
		sealed protected override void draw(){
			scaledUnit = BLOCK_SIZE*scale; //called so because a block is a world unit
			float arrowHeadBreadth = scaledUnit/4f;
			float arrowHeadLength = scaledUnit-arrowHeadBreadth;
			
			
			//Drawing table and grid lines
			pen.Color = Color.Black;
			pen.Width = 1.2f;
			
			if(tileCount != 0){
				var loc = new PointF();
				
				for(int y=0; y<WORLD_HEIGHT; y++){
					for(int x=0; x<WORLD_WIDTH; x++){
						int i = y*WORLD_WIDTH + x;
						
						loc.X = worldOrigin.X + x*scaledUnit;
						loc.Y = worldOrigin.Y + y*scaledUnit;
							
						if(tableObjects[i] != -1)
							g.DrawImage(symbol[tableObjectSymbolId[i]], loc.X, loc.Y, scaledUnit, scaledUnit);
						else
							g.DrawImage(NO_SYMBOL, loc.X, loc.Y, scaledUnit, scaledUnit);
						
						g.DrawRectangle(pen, loc.X, loc.Y, scaledUnit, scaledUnit);
					}
				}
			}
			
			//Drawing the brush
			sBrush.Color = Color.LightGray;
			g.FillEllipse(sBrush, brushPos.X-scaledUnit/4f, brushPos.Y-scaledUnit/4f, scaledUnit/2f, scaledUnit/2f);
			
			pen.Width = 1.75f;
			//Draw Y axis
			pen.Color = Color.Green;
			sBrush.Color = Color.Green;
			g.DrawLine(pen, worldOrigin.X,worldOrigin.Y, worldOrigin.X, worldOrigin.Y+scaledUnit);
			g.DrawLine(pen, worldOrigin.X, worldOrigin.Y+scaledUnit, worldOrigin.X+arrowHeadBreadth, worldOrigin.Y+arrowHeadLength);
			g.DrawLine(pen, worldOrigin.X, worldOrigin.Y+scaledUnit, worldOrigin.X-arrowHeadBreadth, worldOrigin.Y+arrowHeadLength);
			
			g.DrawString("Y", defaultFont, sBrush, worldOrigin.X-15, worldOrigin.Y);
			//Draw X axis
			pen.Color = Color.Red;
			sBrush.Color = Color.Red;
			g.DrawLine(pen, worldOrigin.X, worldOrigin.Y, worldOrigin.X+scaledUnit, worldOrigin.Y);
			g.DrawLine(pen, worldOrigin.X+scaledUnit, worldOrigin.Y, worldOrigin.X+arrowHeadLength, worldOrigin.Y+arrowHeadBreadth);
			g.DrawLine(pen, worldOrigin.X+scaledUnit, worldOrigin.Y, worldOrigin.X+arrowHeadLength, worldOrigin.Y-arrowHeadBreadth);
			
			g.DrawString("X", defaultFont, sBrush, worldOrigin.X, worldOrigin.Y-15);
			
			//Draw final image
			gfx.DrawImage(bufferImage,0,0);
		}
		
		public override void Dispose(){
			NO_SYMBOL.Dispose();
			
			for(int i=0; i<symbol.Length; i++)
				symbol[i].Dispose();
			
			if(tableObjects != null)
				tableObjects.Clear();
			if(tableObjectSymbolId != null)
				tableObjectSymbolId.Clear();
			
			base.Dispose();
		}
		
		//Editor operations
		public override void changedSelection(int id){
			currentObject = listLink[id];
			currentObjectSymbolId = id;
			updateStatusBar();
		}
		
		private void placeWorldObject(int x, int y){
			if(x < 0 || y < 0 || x >= WORLD_WIDTH || y >= WORLD_HEIGHT)
				return;
			
			int a = y*WORLD_WIDTH+x;
			
			tableObjects[a] = currentObject;
			tableObjectSymbolId[a] = currentObjectSymbolId;
		}
		private void eraseWorldObject(int x, int y){
			if(x < 0 || y < 0 || x >= WORLD_WIDTH || y >= WORLD_HEIGHT)
				return;
			
			tableObjects[y*WORLD_WIDTH+x] = emptyObject;
		}
		
		//Inputs
		public override void mouseMoved(Point mousePos, Point lastMousePos){
			brushPos.X = mousePos.X;
			brushPos.Y = mousePos.Y;
			
			mouseWorldPos.X = (int)((mousePos.X - worldOrigin.X)/scaledUnit);
			mouseWorldPos.Y = (int)((mousePos.Y - worldOrigin.Y)/scaledUnit);
			
			updateStatusBar();
		}
		
		public override void mouseDragged(Point mousePos, Point lastMousePos, MouseButtons button){
			if(button == MouseButtons.Middle){
				worldOrigin.X += mousePos.X - lastMousePos.X;
				worldOrigin.Y += mousePos.Y - lastMousePos.Y;
			}
			else if(button == MouseButtons.Left)
				placeWorldObject((int)((mousePos.X - worldOrigin.X)/scaledUnit),(int)((mousePos.Y - worldOrigin.Y)/scaledUnit));
			else if(button == MouseButtons.Right)
				eraseWorldObject((int)((mousePos.X - worldOrigin.X)/scaledUnit),(int)((mousePos.Y - worldOrigin.Y)/scaledUnit));
		}
		
		public override void mouseUp(Point mousePos, MouseButtons button){
			if(button == MouseButtons.Left)
				placeWorldObject((int)((mousePos.X - worldOrigin.X)/scaledUnit),(int)((mousePos.Y - worldOrigin.Y)/scaledUnit));
			else if(button == MouseButtons.Right)
				eraseWorldObject((int)((mousePos.X - worldOrigin.X)/scaledUnit),(int)((mousePos.Y - worldOrigin.Y)/scaledUnit));
		}
		
		public override void mouseDown(Point mousePos, MouseButtons button){}
		
		//File operations
		public override void newMap(){
			tileCount = WORLD_TABLE_SIZE;
			
			tableObjects.Clear();
			tableObjectSymbolId.Clear();
			for(int i=0; i<tileCount; i++){
				tableObjects.Add(emptyObject);
				tableObjectSymbolId.Add(0);
			}
			
			worldOrigin.X = HALF_WIDTH;
			worldOrigin.Y = HALF_HEIGHT;
		}
		
		public override void loadMap(String path){
			byte[] bytes = File.ReadAllBytes(path);
			int p = 0;
			
			tileCount = WORLD_TABLE_SIZE;
			
			//Reinitialize table
			tableObjects.Clear();
			tableObjectSymbolId.Clear();
			for(int i=0; i<tileCount; i++){
				tableObjectSymbolId.Add(0);
				tableObjects.Add(emptyObject);
			}
			
			int objectCount = readInteger(ref bytes,ref p);
			
			Console.Write("Object count: {0} \n",objectCount);
			
			//Place objects in table
			//WorldObject o = new WorldObject();
			for(int i=0; i<objectCount; i++){
				
				int id  = readInteger(ref bytes,ref p);
				int x = readInteger(ref bytes,ref p);
				int y = readInteger(ref bytes,ref p);
				
				int j = x+y*WORLD_WIDTH;
				
				tableObjects[j] = id;
				tableObjectSymbolId[j] = id;
			
				Console.Write("OBJECT: ID: {0} X:{1} Y:{2} \n", id,x,y);
			}
			
		}
		public override void saveMap(String path){
			
			//Count how many objects are
			int objectCount = 0;
			foreach(int x in tableObjects)
				if(x != -1)
					objectCount++;
			
			//Calculate how much space is required
			int size = (12 * objectCount) + 4;
			
			//Write data into byte array
			byte[] bytes = new byte[size];
			int p = 0;
			
			putInByteArray(objectCount, ref bytes, ref p);
			
			for(int i=0; i < tileCount; i++){
				if(tableObjects[i] == -1)
					continue;
				
				int id = tableObjects[i];
				int xPos = i % WORLD_WIDTH;
				int yPos = i / WORLD_WIDTH;
						
				putInByteArray(id,ref bytes, ref p);
				putInByteArray(xPos,ref bytes, ref p);
				putInByteArray(yPos,ref bytes, ref p);
			}
			
			//Writing to file
			File.WriteAllBytes(path, bytes);
		}
		
		//Other
		private void updateStatusBar(){
			statusBar.Text = String.Format(statusBarFormat, mouseWorldPos.X, mouseWorldPos.Y, scale, objectNames[currentObject]);
		}
		
		private void putInByteArray(int x, ref byte[] bytes, ref int p){
				bytes[p++] = (byte)((x >> 24) & 0xFF);
				bytes[p++] = (byte)((x >> 16) & 0xFF);
				bytes[p++] = (byte)((x >> 8) & 0xFF);
				bytes[p++] = (byte)(x & 0xFF);
		}
		
		private int readInteger(ref byte[] bytes, ref int p){
			int x = (int)( ((bytes[p++] & 0xFF)  << 24) | ((bytes[p++] & 0xFF) << 16) | ((bytes[p++] & 0xFF) << 8) | (bytes[p++] & 0xFF));
			return x;
		}
		
		
	}//End of class
	
}//End of namespace
