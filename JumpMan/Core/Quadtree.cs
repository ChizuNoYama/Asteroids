using System.Collections.Generic;
using System.Linq;
using JumpMan.Models;
using Microsoft.Xna.Framework;

namespace JumpMan.Core
{
    public class Quadtree
    {
        public Quadtree(int x, int  y, int width, int height, int capacity = 0)
        {
            _cellSize = new Rectangle(x, y, width,height);
            _capacity = capacity;
        }
        
        // This should be half the screen size length and width wise
        private Rectangle _cellSize;
        private int _capacity;
        private bool _divided;
        
        public List<SpawnableObject> TotalItems { get; protected set; }
        public Quadtree TopLeft { get; protected set; }
        public Quadtree TopRight { get; protected set; }
        public Quadtree BottomRight { get; protected set; }
        public Quadtree BottomLeft { get; protected set; }
        
        public void AddItem(SpawnableObject item)
        {
            if (!_cellSize.Contains(item.Position))
            {
                return;
            }
            
            if (this.TotalItems.Count < _capacity)
            {
                this.TotalItems.Add(item);
            }
            else
            {
                if (!_divided)
                {
                    this.Subdivide();
                    this.TopLeft.AddItem(item);
                    this.TopRight.AddItem(item);
                    this.BottomLeft.AddItem(item);
                    this.BottomRight.AddItem(item);
                    _divided = true;
                }
            }
        }

        private void Subdivide()
        {
            this.TopLeft = new Quadtree(x: _cellSize.X/2, y: _cellSize.Y/2, width: _cellSize.Width/2, height:_cellSize.Height/2);
            this.TopRight = new Quadtree(x: _cellSize.X/2 + _cellSize.X, y: _cellSize.Y/2, width: _cellSize.Width/2, height:_cellSize.Height/2);
            this.BottomLeft = new Quadtree(x: _cellSize.X/2, y: _cellSize.Y/2 + _cellSize.Y, width: _cellSize.Width/2, height:_cellSize.Height/2);
            this.BottomRight = new Quadtree(x: _cellSize.X/2 + _cellSize.X, y: _cellSize.Y/2 + _cellSize.Y, width: _cellSize.Width/2, height:_cellSize.Height/2);
        }

        public List<SpawnableObject> Query(Rectangle searchArea)
        {
            List<SpawnableObject> foundItems = new List<SpawnableObject>();
            if (this._cellSize.Intersects(searchArea))
            {
                if (_divided)
                {
                    foundItems.AddRange(this.TopLeft.Query(searchArea));
                    foundItems.AddRange(this.TopRight.Query(searchArea));
                    foundItems.AddRange(this.BottomLeft.Query(searchArea));
                    foundItems.AddRange(this.BottomRight.Query(searchArea));
                }
                else
                {
                    foreach (SpawnableObject item in this.TotalItems)
                    {
                        if (searchArea.Contains(item.Position))
                        {
                            foundItems.Add(item);
                        }
                    }
                }
            }
            
            return foundItems;
        }
    }
}