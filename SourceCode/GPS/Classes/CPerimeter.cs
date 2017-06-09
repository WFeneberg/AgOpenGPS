﻿using System.Collections.Generic;
using SharpGL;
using System.Windows;
using System.Drawing;

namespace AgOpenGPS
{
    public class CPerimeter
    {
        //copy of the mainform address
        private FormGPS mf;
        private OpenGL gl;

        //list of coordinates of perimter line
        public List<vec2> periPtList = new List<vec2>();
        public List<vec2> calcList = new List<vec2>();

        //list of the list of patch data individual triangles for that entire section activity
        public List<List<vec2>> periLineList = new List<List<vec2>>();

        //button status
        public bool isBtnPerimeterOn = false;

        //area variable
        public double area = 0;

       //constructor
        public CPerimeter(OpenGL gl, FormGPS f)
        {            
            this.mf = f;
            this.gl = gl;
        }

        public void DrawPerimeterLine()
        {            
            ////draw the perimeter line so far
            int ptCount = periPtList.Count;
            if (ptCount < 1) return;
            gl.LineWidth(2);
            gl.Color(0.2f, 0.98f, 0.0f);
            gl.Begin(OpenGL.GL_LINE_STRIP);
            for (int h = 0; h < ptCount; h++) gl.Vertex(periPtList[h].easting, 0, periPtList[h].northing);
            gl.End();

            //the "close the loop" line
            gl.LineWidth(1);
            gl.Color(0.98f, 0.8f, 0.0f);
            gl.Begin(OpenGL.GL_LINE_STRIP);
            gl.Vertex(periPtList[ptCount-1].easting, 0, periPtList[ptCount-1].northing);
            gl.Vertex(periPtList[0].easting, 0, periPtList[0].northing);
            gl.End();
            
             
            area = 0;         // Accumulates area in the loop

            int j = ptCount-1;  // The last vertex is the 'previous' one to the first

            for (int i=0; i<ptCount; j = i++)
            {
                area = area + (periPtList[j].easting + periPtList[i].easting) * (periPtList[j].northing - periPtList[i].northing); 
            }
     
            area = System.Math.Abs(area/2);
        }
    }
}
