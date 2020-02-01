using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomScripts.Environment
{
    public class Tile : MonoBehaviour
    {
        public bool Marked { get; set; }
        private Color markerColor;
        private Material material;

        private void Awake()
        {
            this.markerColor = Color.green;
            this.material = GetComponent<Renderer>().material;
        }

        public void MarkColor()
        {
            this.material.color = markerColor;      
        }
    }
}
