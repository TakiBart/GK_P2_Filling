using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_P2_Filling
{
    public class MyEdge
    {
        public float YMax { get; set; }
        public float XMin { get; set; }
        public float Coefficient { get; set; }
        public MyEdge Next { get; set; }

        public MyEdge()
        {
            YMax = 0;
            XMin = 0;
            Coefficient = 0;
            Next = null;
        }

        public MyEdge(float _YMax, float _XMin, float _Coef, MyEdge _Next)
        {
            YMax = _YMax;
            XMin = _XMin;
            Coefficient = _Coef;
            Next = _Next;
        }
    }
}
