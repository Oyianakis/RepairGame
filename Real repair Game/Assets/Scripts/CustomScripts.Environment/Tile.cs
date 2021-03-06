﻿using System;
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
        [SerializeField] private Material changedMat;

        private void Awake()
        {
            this.markerColor = Color.green;
            this.material = GetComponent<Renderer>().material;
        }

        public void MarkColor()
        {
            //Should probably change how to compare these nodes, possibly when we get more node data?
            if (this.GetComponent<Renderer>().material.color != changedMat.color)
            {
                this.material.color = markerColor;
                this.GetComponent<Renderer>().material = changedMat;
            }
            else
            {
                this.transform.Rotate(0, 0, -90);
            }
        }

    }

}

