using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Drawing.Drawing2D;

namespace GK_P2_Filling
{

    public partial class Form1 : Form
    {

        public PointF[] Points = new PointF[] { new PointF(50,50), new PointF(300,35), new PointF(75, 400),
                                                new PointF(60,250), new PointF(400,250), new PointF(300, 90)};
        // First three are first triangle
        
        public MyEdge[] GET;
        public MyEdge[] GET2;
        public MyEdge AET;
        public MyEdge AET2;

        // D - zaburzenie
        float[] D = { 0, 0, 0 };

        // L - wersor do światła
        float[] L = { 0, 0, 1 };

        // N - wektor normalny 
        float[] N = { 0, 0, 1 };

        // Np - wektor normalny z zaburzeniem
        float[] Np = { 0, 0, 1 };

        // lightPos - położenie źródła światła
        float[] lightPos = { 0, 0, 1 };

        DirectBitmap tri1ColText;
        DirectBitmap tri2ColText;
        DirectBitmap normVectText;
        DirectBitmap disturbText;
        
        int iChosen;
        bool isChosen = false;

        bool isBeingMoved = false;
        bool isTriangleBeingMoved = false;
        Point mousePos;

        float angle = 0;
        float rad;
        int r = 40;
        int refHeight = 200;
        int K = 100;


        public Form1()
        {
            InitializeComponent();
            WorkspacePictureBox.Image = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);
            WorkspacePictureBox.Refresh();
            
            GET = new MyEdge[WorkspacePictureBox.Height];
            GET2 = new MyEdge[WorkspacePictureBox.Height];
            
            tri1ColText = new DirectBitmap(Properties.Resources.brick_normalmap);
            Tri1ColTextPB.BackgroundImage = tri1ColText.Bitmap;
            
            tri2ColText = new DirectBitmap(Properties.Resources.normal_map);
            Tri2ColTextPB.BackgroundImage = tri2ColText.Bitmap;
            
            normVectText = new DirectBitmap(Properties.Resources.normal_map);
            NormVectTextPB.BackgroundImage = normVectText.Bitmap;
            
            disturbText = new DirectBitmap(Properties.Resources.brick_normalmap);
            DisturbTextPB.BackgroundImage = disturbText.Bitmap;
            
            timer1.Start();

            FillScanLines();
            
        }
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            double ratioX = (double)maxWidth / image.Width;
            double ratioY = (double)maxHeight / image.Height;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);

            Bitmap newImage = new Bitmap(newWidth, newHeight);

            using (Graphics graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        private void RedrawGraph()
        {
            //   WorkspacePictureBox.Refresh();
            Pen pen = new Pen(Color.Black, 3);
            RectangleF circ = new RectangleF();
            SolidBrush brush = new SolidBrush(Color.Black);
            circ.Size = new Size(r, r);

            FillScanLines();
            
            brush.Dispose();
            pen.Dispose();
        }

        private void WorkspacePictureBox_SizeChanged(object sender, EventArgs e)
        {
            Image nIm = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);

            using (Graphics g = Graphics.FromImage(nIm))
            {
                g.DrawImage(WorkspacePictureBox.Image, new Point(0, 0));
                FillScanLines();
            }
            WorkspacePictureBox.Image.Dispose();
            WorkspacePictureBox.Image = nIm;
        }


        private void WorkspacePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(WorkspacePictureBox.Image);
            Point location = new Point(e.X - r / 2, e.Y - r / 2);
            if (e.Button == MouseButtons.Left)
            {

                if (Points.Length <= 0)
                    return;

                Rectangle circ = new Rectangle();
                int iClosest = 0;
                int closestDist = (int)(Math.Pow(Math.Abs(Points[iClosest].X + r / 2 - e.X), 2) + Math.Pow(Math.Abs(Points[iClosest].Y + r / 2 - e.Y), 2));

                circ.Size = new Size(r, r);
                using (g)
                {
                    for(int i = 0; i < Points.Length; i++)
                    {
                        if (Math.Pow(Math.Abs(Points[i].X + r / 2 - e.X), 2) + Math.Pow(Math.Abs(Points[i].Y + r / 2 - e.Y), 2) < closestDist)
                        {
                            iClosest = i;
                            closestDist = (int)(Math.Pow(Math.Abs(Points[i].X + r / 2 - e.X), 2) + Math.Pow(Math.Abs(Points[i].Y + r / 2 - e.Y), 2));
                        }

                    }

                    iChosen = iClosest;
                    isBeingMoved = true;

                    if (closestDist < 800)
                    {
                        isTriangleBeingMoved = false;
                        isChosen = true;
                        //iChosen = iClosest;
                        //isBeingMoved = true;
                    }
                    else
                    {
                        isTriangleBeingMoved = true;
                        isChosen = false;
                    }
                    WorkspacePictureBox.Refresh();
                    FillScanLines();
                }
                mousePos = e.Location;
            }
            
        }

        private void WorkspacePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isBeingMoved || isTriangleBeingMoved)   // && isChosen
                {
                    isBeingMoved = false;
                    isTriangleBeingMoved = false;
                    float nX = Points[iChosen].X;
                    float nY = Points[iChosen].Y;
                    int i = iChosen;
                    if (Points[iChosen].X < 0)
                        nX = -r / 2;
                    else if (Points[iChosen].X > WorkspacePictureBox.Width)
                        nX = WorkspacePictureBox.Width - r / 2;

                    if (Points[iChosen].Y < 0)
                        nY = -r / 2;
                    else if (Points[iChosen].Y > WorkspacePictureBox.Height)
                        nY = WorkspacePictureBox.Height - r / 2;

                    Points[iChosen].X = nX;
                    Points[iChosen].Y = nY;

                    mousePos = e.Location;
                    
                    WorkspacePictureBox.Refresh();
                    FillScanLines();
                }
                
            }
        }
        

        private void WorkspacePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (isBeingMoved)
            {
                if (isChosen)
                {
                    int i = iChosen;
                    float nX = Points[iChosen].X - mousePos.X + e.X;
                    float nY = Points[iChosen].Y - mousePos.Y + e.Y;
                    Points[iChosen].X = nX;
                    Points[iChosen].Y = nY;
                }
                else if (isTriangleBeingMoved)    // Moving all triangle
                {
                    int ind = iChosen < 3 ? 0 : 3;
                    for (int i = 0; i < Points.Length / 2; i++)
                    {
                        Points[i + ind].X = Points[i + ind].X - mousePos.X + e.X;
                        Points[i + ind].Y = Points[i + ind].Y - mousePos.Y + e.Y;
                    }
                }
                mousePos = e.Location;
              
                FillScanLines();
            }
        }
        
        public void UpdateEdges()
        {
            float x1 = Points[0].X, y1 = Points[0].Y;
            float x2 = Points[1].X, y2 = Points[1].Y;
            float coef = (x2 - x1) / (y2 - y1);
            MyEdge temp = new MyEdge(Math.Max(Points[0].Y, Points[1].Y), Points[Points[0].Y < Points[1].Y ? 0 : 1].X, coef, null);
            int iNext;

            float x3 = Points[3].X, y3 = Points[3].Y;
            float x4 = Points[4].X, y4 = Points[4].Y;
            float coef2 = (x4 - x3) / (y4 - y3);
            MyEdge temp2 = new MyEdge(Math.Max(Points[3].Y, Points[4].Y), Points[Points[3].Y < Points[4].Y ? 3 : 4].X, coef2, null);
            int iNext2;
            
            for (int i = 0; i < GET.Length; i++)
            {
                GET[i] = null;
                GET2[i] = null;
            }

            GET[(int)Math.Min(Points[0].Y, Points[1].Y)] = temp;
            GET2[(int)Math.Min(y3, y4)] = temp2;

            for (int i = 1; i < Points.Length/2; i++)
            {
                if (i == 2)
                {
                    iNext = 0;
                    iNext2 = 3;
                }
                else
                {
                    iNext = i + 1;
                    iNext2 = iNext + 3;
                }
                x1 = Points[i].X;
                y1 = Points[i].Y;
                x2 = Points[iNext].X;
                y2 = Points[iNext].Y;
                coef = (x2 - x1) / (y2 - y1);
                
                x3 = Points[i+3].X;
                y3 = Points[i+3].Y;
                x4 = Points[iNext2].X;
                y4 = Points[iNext2].Y;
                coef2 = (x4 - x3) / (y4 - y3);
                
                int ind = (int)Math.Min(Points[i].Y, Points[iNext].Y);
                if (Points[i].Y != Points[iNext].Y)
                {
                    if (GET[ind] == null)
                        GET[ind] = new MyEdge(Math.Max(Points[i].Y, Points[iNext].Y), Points[Points[i].Y < Points[iNext].Y ? i : iNext].X, coef, null);
                    else
                    {
                        temp = GET[ind];
                        while (temp.Next != null)
                        {
                            temp = temp.Next;
                        }
                        temp.Next = new MyEdge(Math.Max(Points[i].Y, Points[iNext].Y), Points[Points[i].Y < Points[iNext].Y ? i : iNext].X, coef, null);
                    }
                }

                int ind2 = (int)Math.Min(Points[i + 3].Y, Points[iNext2].Y);
                if (Points[i+3].Y != Points[iNext2].Y)
                {
                    if (GET2[ind2] == null)
                        GET2[ind2] = new MyEdge(Math.Max(Points[i+3].Y, Points[iNext2].Y), Points[Points[i+3].Y < Points[iNext2].Y ? i+3 : iNext2].X, coef2, null);
                    else
                    {
                        temp = GET2[ind2];
                        while (temp.Next != null)
                        {
                            temp = temp.Next;
                        }
                        temp.Next = new MyEdge(Math.Max(Points[i+3].Y, Points[iNext2].Y), Points[Points[i+3].Y < Points[iNext2].Y ? i+3 : iNext2].X, coef2, null);
                    }
                }
            }
        }

        public void FillScanLines()
        {
            int y = 0;
            int R, G, B;

            float cosNL;
            float lighVectLen;
            float normVectLen;
            float distLen;
            float refVectLen;
            float refPointVectLen;
            float cosVrVp;

            float[] Vr = new float[3];
            float[] Vp = new float[3];

            Point RefPos = new Point(WorkspacePictureBox.Image.Width / 2, WorkspacePictureBox.Image.Height / 2);

            Color objCol;
            Color objCol2;
            Color ligCol = LightColorBoxPB.BackColor;
            Color normVectCol;
            Color distPix;
            Color distNX;
            Color distNY;

            MyEdge temp = null;
            UpdateEdges();
            Bitmap bitmap = new Bitmap(WorkspacePictureBox.Width, WorkspacePictureBox.Height);

            while (GET[y] == null && GET2[y] == null) 
                y++;
            AET = null;
            AET2 = null;
            while (y < WorkspacePictureBox.Height) 
            {
                // Add lists from y bucket to AET
                if (GET[y] != null && AET == null)
                    AET = GET[y];
                else if (AET != null)
                {
                    if (GET[y] != null)
                    {
                        temp = AET;
                        while (temp.Next != null)
                            temp = temp.Next;
                        temp.Next = GET[y];
                    }
                    
                    // Sort AET by x
                    AET = MergeSort(AET);

                    // TO_TEST Remove from AET elems for which y = yMax
                    temp = AET;
                    while (temp != null && temp.Next != null)
                    {
                        if (temp.Next.YMax <= y)
                            temp.Next = temp.Next.Next;
                        temp = temp.Next;
                    }

                    if (AET.YMax <= y)
                        AET = AET.Next;

                    if (AET!= null && AET.YMax <= y)
                        AET = AET.Next;

                    // Fill pixs between crossings
                    temp = AET;
                    
                    while (temp != null && temp.Next != null)
                    {
                        for (int i = (int)temp.XMin; i < temp.Next.XMin; i++)
                        {
                            if (Tri1ColConstRB.Checked)
                                objCol = Tri1ColorBoxPB.BackColor;
                            else    // From texture
                                objCol = tri1ColText.GetPixel(i % (tri1ColText.Width - 1) + 1, y % (tri1ColText.Height - 1) + 1);

                            Point mouse = PointToClient(MousePosition);

                            // Bąbelek aka "bubble"
                            if (BubbleCB.Checked && (i - PointToClient(MousePosition).X) * (i - PointToClient(MousePosition).X) + (y - PointToClient(MousePosition).Y) * (y - PointToClient(MousePosition).Y) <= r * r)
                            {
                                // Oblicznie wektorów bąbelka
                                N[2] = (float)Math.Sqrt(r*r - Math.Sqrt(i*i+y*y));
                                N[0] = (i - mouse.X) / N[2];
                                N[1] = (y - mouse.Y) / N[2];
                                N[2] /= N[2];
                            }
                            else if (NormalVectTextRB.Checked)
                            {
                                normVectCol = normVectText.GetPixel(i % (normVectText.Width), y % (normVectText.Height));
                                N[2] = normVectCol.B;
                                N[0] = (normVectCol.R - 127) / N[2];
                                N[1] = (normVectCol.G - 127) / N[2];
                                N[2] /= N[2];
                            }
                            else
                                N = new float[] { 0, 0, 1 };
                            
                            if (DisturbTextRB.Checked)
                            {
                                distPix = disturbText.GetPixel(i % (disturbText.Width), y % (disturbText.Height));
                                distNX = disturbText.GetPixel((i + 1) % (disturbText.Width), y % (disturbText.Height));
                                distNY = disturbText.GetPixel(i % (disturbText.Width), (y + 1) % (disturbText.Height));

                                D[0] = 1 * ((distNX.R + distNX.G + distNX.B) - (distPix.R + distPix.G + distPix.B)) / 3;
                                D[1] = 1 * ((distNY.R + distNY.G + distNY.B) - (distPix.R + distPix.G + distPix.B)) / 3;
                                D[2] = -N[0] * ((distNX.R + distNX.G + distNX.B) - (distPix.R + distPix.G + distPix.B)) / 3 - N[1] * ((distNY.R + distNY.G + distNY.B) - (distPix.R + distPix.G + distPix.B)) / 3;
                                distLen = (float)Math.Sqrt(D[0] * D[0] + D[1] * D[1] + D[2] * D[2]);
                                if (distLen != 0)
                                {
                                    D[0] /= distLen;
                                    D[1] /= distLen;
                                    D[2] /= distLen;
                                }
                            }
                            
                            Np[0] = N[0] + D[0];
                            Np[1] = N[1] + D[1];
                            Np[2] = N[2] + D[2];
                            normVectLen = (float)Math.Sqrt(Np[0] * Np[0] + Np[1] * Np[1] + Np[2] * Np[2]);

                            Np[0] /= normVectLen;
                            Np[1] /= normVectLen;
                            Np[2] /= normVectLen;

                            if(LighSourVectAnimRB.Checked) 
                            {
                                L[0] = lightPos[0] - i;
                                L[1] = lightPos[1] - y;
                                L[2] = 100f;
                                lighVectLen = (float)Math.Sqrt(L[0] * L[0] + L[1] * L[1] + L[2] * L[2]);

                                L[0] /= lighVectLen;
                                L[1] /= lighVectLen;
                                L[2] /= lighVectLen;
                            }

                            cosNL = Math.Max(0,(Np[0] * L[0] + Np[1] * L[1] + Np[2] * L[2]));

                            // Normalized (both divided by 255) and denormalized (result multiplied by 255)
                            R = (int)(objCol.R / 255f * ligCol.R * cosNL);
                            G = (int)(objCol.G / 255f * ligCol.G * cosNL);
                            B = (int)(objCol.B / 255f * ligCol.B * cosNL);

                            if (ReflectorCB.Checked)
                            {
                                Vr[0] = mouse.X - RefPos.X;
                                Vr[1] = mouse.Y - RefPos.Y;
                                Vr[2] = refHeight;
                                refVectLen = (float)Math.Sqrt(Vr[0] * Vr[0] + Vr[1] * Vr[1] + Vr[2] * Vr[2]);
                                Vr[0] /= refVectLen;
                                Vr[1] /= refVectLen;
                                Vr[2] /= refVectLen;

                                Vp[0] = i - RefPos.X;
                                Vp[1] = y - RefPos.Y;
                                Vp[2] = refHeight;
                                refPointVectLen = (float)Math.Sqrt(Vp[0] * Vp[0] + Vp[1] * Vp[1] + Vp[2] * Vp[2]);
                                Vp[0] /= refPointVectLen;
                                Vp[1] /= refPointVectLen;
                                Vp[2] /= refPointVectLen;

                                cosVrVp = Vr[0] * Vp[0] + Vr[1] * Vp[1] + Vr[2] * Vp[2];
                                cosVrVp = (float)Math.Pow(cosVrVp, K);
                                R = (int)(R * (1+cosVrVp));
                                G = (int)(G * (1+cosVrVp));
                                B = (int)(B * (1+cosVrVp));
                                if (R > 255) R = 255;
                                if (G > 255) G = 255;
                                if (B > 255) B = 255;
                            }

                            bitmap.SetPixel(i, y, Color.FromArgb(R,G,B));
                        }
                        temp = temp.Next.Next;
                    }
                    // For each edge in AET update x (x+=1/m)
                    temp = AET;
                    while (temp != null)
                    {
                        temp.XMin += temp.Coefficient;
                        temp = temp.Next;
                    }
                }

                // TRIANGLE no 2, not DRY
                temp = null;
                // Add lists from y bucket to AET
                if (GET2[y] != null && AET2 == null)
                    AET2 = GET2[y];
                else if (AET2 != null)
                {
                    if (GET2[y] != null)
                    {
                        temp = AET2;
                        while (temp.Next != null)
                            temp = temp.Next;
                        temp.Next = GET2[y];
                    }

                    // Sort AET by x
                    AET2 = MergeSort(AET2);

                    // TO_TEST Remove from AET elems for which y = yMax
                    temp = AET2;
                    while (temp != null && temp.Next != null)
                    {
                        if (temp.Next.YMax <= y)
                            temp.Next = temp.Next.Next;
                        temp = temp.Next;
                    }

                    if (AET2.YMax <= y)
                        AET2 = AET2.Next;

                    if (AET2 != null && AET2.YMax <= y)
                        AET2 = AET2.Next;

                    // Fill pixs between crossings
                    temp = AET2;

                    while (temp != null && temp.Next != null)
                    {
                        for (int i = (int)temp.XMin; i < temp.Next.XMin; i++)
                        {
                            if (Tri2ColConstRB.Checked)
                                objCol2 = Tri2ColorBoxPB.BackColor;
                            else    // From texture
                                objCol2 = tri2ColText.GetPixel(i % (tri2ColText.Width - 1) + 1, y % (tri2ColText.Height - 1) + 1);

                            Point mouse = PointToClient(MousePosition);
                            // Bąbelek aka "bubble"
                            if (BubbleCB.Checked && (i - PointToClient(MousePosition).X) * (i - PointToClient(MousePosition).X) + (y - PointToClient(MousePosition).Y) * (y - PointToClient(MousePosition).Y) <= r * r)
                            {
                               

                                N[2] = (float)Math.Sqrt(r * r - Math.Sqrt(i * i + y * y));
                                N[0] = (i - mouse.X) / N[2];
                                N[1] = (y - mouse.Y) / N[2];
                                N[2] /= N[2];

                            }
                            else if (NormalVectTextRB.Checked)
                            {
                                normVectCol = normVectText.GetPixel(i % (normVectText.Width), y % (normVectText.Height));
                                N[2] = normVectCol.B;
                                N[0] = (normVectCol.R - 127) / N[2];
                                N[1] = (normVectCol.G - 127) / N[2];
                                N[2] /= N[2];
                            }
                            else
                                N = new float[] { 0, 0, 1 };
                            
                            if (DisturbTextRB.Checked)
                            {
                                distPix = disturbText.GetPixel(i % (disturbText.Width), y % (disturbText.Height));
                                distNX = disturbText.GetPixel((i + 1) % (disturbText.Width), y % (disturbText.Height));
                                distNY = disturbText.GetPixel(i % (disturbText.Width), (y + 1) % (disturbText.Height));

                                D[0] = 1 * ((distNX.R + distNX.G + distNX.B) - (distPix.R + distPix.G + distPix.B)) / 3;
                                D[1] = 1 * ((distNY.R + distNY.G + distNY.B) - (distPix.R + distPix.G + distPix.B)) / 3;
                                D[2] = -N[0] * ((distNX.R + distNX.G + distNX.B) - (distPix.R + distPix.G + distPix.B)) / 3 - N[1] * ((distNY.R + distNY.G + distNY.B) - (distPix.R + distPix.G + distPix.B)) / 3;
                                distLen = (float)Math.Sqrt(D[0] * D[0] + D[1] * D[1] + D[2] * D[2]);
                                if (distLen != 0)
                                {
                                    D[0] /= distLen;
                                    D[1] /= distLen;
                                    D[2] /= distLen;
                                }
                            }


                            Np[0] = N[0] + D[0];
                            Np[1] = N[1] + D[1];
                            Np[2] = N[2] + D[2];
                            normVectLen = (float)Math.Sqrt(Np[0] * Np[0] + Np[1] * Np[1] + Np[2] * Np[2]);

                            Np[0] /= normVectLen;
                            Np[1] /= normVectLen;
                            Np[2] /= normVectLen;

                            if (LighSourVectAnimRB.Checked)
                            {
                                L[0] = lightPos[0] - i;
                                L[1] = lightPos[1] - y;
                                L[2] = 100f;
                                lighVectLen = (float)Math.Sqrt(L[0] * L[0] + L[1] * L[1] + L[2] * L[2]);

                                L[0] /= lighVectLen;
                                L[1] /= lighVectLen;
                                L[2] /= lighVectLen;
                            }

                            cosNL = Math.Max(0, (Np[0] * L[0] + Np[1] * L[1] + Np[2] * L[2]));

                            // Normalized (both divided by 255) and denormalized (result multiplied by 255)
                            R = (int)(objCol2.R / 255f * ligCol.R * cosNL);
                            G = (int)(objCol2.G / 255f * ligCol.G * cosNL);
                            B = (int)(objCol2.B / 255f * ligCol.B * cosNL);


                            if (ReflectorCB.Checked)
                            {
                                Vr[0] = mouse.X - RefPos.X;
                                Vr[1] = mouse.Y - RefPos.Y;
                                Vr[2] = refHeight;
                                refVectLen = (float)Math.Sqrt(Vr[0] * Vr[0] + Vr[1] * Vr[1] + Vr[2] * Vr[2]);
                                Vr[0] /= refVectLen;
                                Vr[1] /= refVectLen;
                                Vr[2] /= refVectLen;

                                Vp[0] = i - RefPos.X;
                                Vp[1] = y - RefPos.Y;
                                Vp[2] = refHeight;
                                refPointVectLen = (float)Math.Sqrt(Vp[0] * Vp[0] + Vp[1] * Vp[1] + Vp[2] * Vp[2]);
                                Vp[0] /= refPointVectLen;
                                Vp[1] /= refPointVectLen;
                                Vp[2] /= refPointVectLen;

                                cosVrVp = Vr[0] * Vp[0] + Vr[1] * Vp[1] + Vr[2] * Vp[2];
                                cosVrVp = (float)Math.Pow(cosVrVp, K);
                                R = (int)(R * (1 + cosVrVp));
                                G = (int)(G * (1 + cosVrVp));
                                B = (int)(B * (1 + cosVrVp));
                                if (R > 255) R = 255;
                                if (G > 255) G = 255;
                                if (B > 255) B = 255;
                            }

                            bitmap.SetPixel(i, y, Color.FromArgb(R, G, B));
                        }
                        temp = temp.Next.Next;
                    }
                    // For each edge in AET update x (x+=1/m)
                    temp = AET2;
                    while (temp != null)
                    {
                        temp.XMin += temp.Coefficient;
                        temp = temp.Next;
                    }
                }
                y++;
            }
            WorkspacePictureBox.Image.Dispose();
            WorkspacePictureBox.Image = bitmap;
            
        }

        public MyEdge MergeSort(MyEdge head)
        {
            if (head == null || head.Next == null) { return head; }
            MyEdge middle = GetMiddle(head);      //get the middle of the list
            MyEdge sHalf = middle.Next;
            middle.Next = null;   //split the list into two halfs

            return Merge(MergeSort(head), MergeSort(sHalf));  //recurse on that
        }

        //Merge subroutine to merge two sorted lists
        public MyEdge Merge(MyEdge a, MyEdge b)
        {
            MyEdge dummyHead = new MyEdge();
            MyEdge curr = dummyHead;
            while (a != null && b != null)
            {
                if (a.XMin <= b.XMin) { curr.Next = a; a = a.Next; }
                else { curr.Next = b; b = b.Next; }
                curr = curr.Next;
            }
            curr.Next = (a == null) ? b : a;
            return dummyHead.Next;
        }

        //Finding the middle element of the list for splitting
        public MyEdge GetMiddle(MyEdge head)
        {
            if (head == null) { return head; }
            MyEdge slow, fast; slow = fast = head;
            while (fast.Next != null && fast.Next.Next != null)
            {
                slow = slow.Next; fast = fast.Next.Next;
            }
            return slow;
        }

        private void WorkspacePictureBox_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Tri1ColorBoxPB_Click(object sender, EventArgs e)
        {
            if (Tri1ColConstRB.Checked)
            {
                ColorDialog cd = new ColorDialog();

                if (cd.ShowDialog() == DialogResult.OK)
                {
                    Tri1ColorBoxPB.BackColor = cd.Color;

                    FillScanLines();
                }
            }
        }

        private void ColorBoxPB_Click(object sender, EventArgs e)
        {
            if (Tri2ColConstRB.Checked)
            {
                ColorDialog cd = new ColorDialog();

                if (cd.ShowDialog() == DialogResult.OK)
                {
                    Tri2ColorBoxPB.BackColor = cd.Color;

                    FillScanLines();
                }
            }
        }

        private void LightColorBoxPB_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                LightColorBoxPB.BackColor = cd.Color;

                FillScanLines();

            }
        }

        private void Tri1ColTextRB_CheckedChanged(object sender, EventArgs e)
        {
            FillScanLines();
        }

        private void ObjColTextRB_CheckedChanged(object sender, EventArgs e)
        {
            // Triangle no 2
            FillScanLines();
        }
        

        private void DisturbNoRB_CheckedChanged(object sender, EventArgs e)
        {
            if (DisturbNoRB.Checked)
            {
                for (int i = 0; i < D.Length; i++)
                {
                    D[i] = 0;
                    Np[i] = N[i] / N[2];
                }
                FillScanLines();
            }
        }

        private void NormalVectConstRB_CheckedChanged(object sender, EventArgs e)
        {
            if(NormalVectConstRB.Checked)
            {
                N = new float[] { 0, 0, 1 };

                for (int i = 0; i<Np.Length; i++)
                    Np[i] = N[i] + D[i];

                for (int i = 0; i < Np.Length; i++)
                    Np[i] = Np[i] / Np[2];

                FillScanLines();
            }
        }
        
        private void LighSourVectConstRB_CheckedChanged(object sender, EventArgs e)
        {
            if (LighSourVectConstRB.Checked)
            {
                L = new float[] { 0, 0, 1 };
                FillScanLines();
            }
        }

        private void LighSourVectAnimRB_CheckedChanged(object sender, EventArgs e)
        {
            if(LighSourVectAnimRB.Checked)
            {
                lightPos = new float[] { 0.5f, 0.25f, 1 };
                FillScanLines();
            }
        }

        public Bitmap OpenImage()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return new Bitmap(dlg.FileName);
                }
            }
            return Properties.Resources.brick_normalmap;
        }
        
        private void Tri1ColTextPB_Click(object sender, EventArgs e)
        {
            if (Tri1ColTextRB.Checked)
            {
                tri1ColText = new DirectBitmap(new Bitmap(ScaleImage(OpenImage(), WorkspacePictureBox.Width,WorkspacePictureBox.Height)));
                Tri1ColTextPB.BackgroundImage = tri1ColText.Bitmap;
                FillScanLines();
            }
        }

        private void ObjColTextPB_Click(object sender, EventArgs e)
        {
            if (Tri2ColTextRB.Checked)
            {
                tri2ColText = new DirectBitmap(OpenImage());
                Tri2ColTextPB.BackgroundImage = tri2ColText.Bitmap;
                FillScanLines();
            }
        }

        private void Tri1ColConstRB_CheckedChanged(object sender, EventArgs e)
        {
            FillScanLines();
        }


        private void NormVectTextPB_Click(object sender, EventArgs e)
        {
            if (NormalVectTextRB.Checked)
            {
                normVectText = new DirectBitmap(OpenImage());
                NormVectTextPB.BackgroundImage = normVectText.Bitmap;
                FillScanLines();
            }
        }

        private void DisturbTextPB_Click(object sender, EventArgs e)
        {
            if (DisturbTextRB.Checked)
            {
                disturbText = new DirectBitmap(OpenImage());
                DisturbTextPB.BackgroundImage = disturbText.Bitmap;
                FillScanLines();
            }
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            rad = Math.Min(WorkspacePictureBox.Height, WorkspacePictureBox.Width) / 3;
            if (LighSourVectAnimRB.Checked)
            {
                lightPos[0] = (float)(rad * Math.Cos(angle * Math.PI / 180f)) + WorkspacePictureBox.Width / 2;
                lightPos[1] = (float)(rad * Math.Sin(angle * Math.PI / 180f)) + WorkspacePictureBox.Height / 2;
                if (angle < 360)
                    angle += 10f;
                else
                    angle = 0;
            }
            FillScanLines();

        }

        private void ReflectorCB_CheckedChanged(object sender, EventArgs e)
        {
            FillScanLines();
        }

        private void ReflectorCB_Click(object sender, EventArgs e)
        {
            if(ReflectorCB.Checked)
            {
                refHeight = (int)Prompt.ShowDialog("Wprowadź wysokość reflektora", "H - wysokość reflektora", refHeight);
                K = (int)Prompt.ShowDialog("Wprowadź potęgę cosinusa dla reflektora", "K - potęga cosinusa", K);
            }
        }
    }

    public static class Prompt
    {
        public static double ShowDialog(string text, string caption, double length)
        {
            Form prompt = new Form();
            prompt.Width = 300;
            prompt.Height = 150;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            NumericUpDown inputBox = new NumericUpDown() { Left = 50, Top = 50, Width = 200, Minimum = 1, Maximum = 9999, Value = (decimal)length };
            Button confirmation = new Button() { Text = "Ok", Left = 150, Width = 100, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.ShowDialog();
            return Decimal.ToDouble(inputBox.Value);
        }
    }
}
