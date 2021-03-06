#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v2)

#region Source code: Greenshot (GPL)

/*
    This file originated from the Greenshot project (GPL). It may or may not have been modified.
    Please do not contact Greenshot about errors with this code. Instead contact the creators of this program.
    URL: http://greenshot.sourceforge.net/
    Code (CVS): http://greenshot.cvs.sourceforge.net/viewvc/greenshot/
*/

#endregion Source code: Greenshot (GPL)

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Greenshot.Configuration;

namespace Greenshot.Drawing
{
    public delegate void SurfaceElementEventHandler(object source, DrawableContainerList element);

    public class Surface : PictureBox
    {
        private AppConfig conf = AppConfig.GetInstance();

        public event SurfaceElementEventHandler MovingElementChanged;

        public enum DrawingModes { None, Rect, Ellipse, Text, Line }

        private int mX;
        private int mY;
        private bool mouseDown = false;
        private DrawableContainer mouseDownElement = null;

        private Image originalImage;
        private Bitmap currentImage = null;

        private DrawableContainerList elements = new DrawableContainerList();

        private DrawableContainerList selectedElements = new DrawableContainerList();
        private DrawableContainer drawingElement = null;

        public DrawingModes DrawingMode = DrawingModes.None;

        public new Color ForeColor
        {
            get
            {
                if (selectedElements.Count > 0) return selectedElements[selectedElements.Count - 1].ForeColor;
                else return conf.Editor_ForeColor;
            }
            set
            {
                selectedElements.ForeColor = value;
                base.ForeColor = value;
            }
        }

        public new Color BackColor
        {
            get
            {
                if (selectedElements.Count > 0) return selectedElements[selectedElements.Count - 1].BackColor;
                else return conf.Editor_BackColor;
            }
            set
            {
                selectedElements.BackColor = value;
                base.BackColor = value;
            }
        }

        public Color GradientColor
        {
            get
            {
                if (selectedElements.Count > 0) return selectedElements[selectedElements.Count - 1].GradientColor;
                else return conf.Editor_GradientColor;
            }
            set
            {
                selectedElements.GradientColor = value;
            }
        }

        public string GradientType
        {
            get
            {
                if (selectedElements.Count > 0) return selectedElements[selectedElements.Count - 1].GradientType;
                else return conf.Editor_GradientType;
            }
            set
            {
                selectedElements.GradientType = value;
            }
        }

        public int Thickness
        {
            get
            {
                if (selectedElements.Count > 0) return selectedElements[selectedElements.Count - 1].Thickness;
                else return conf.Editor_Thickness;
            }
            set
            {
                selectedElements.Thickness = value;
            }
        }

        public ArrowHeads ArrowHead
        {
            get
            {
                if (selectedElements.Count > 0) return selectedElements[selectedElements.Count - 1].ArrowHeads;
                else return conf.Editor_ArrowHeads;
            }
            set
            {
                selectedElements.ArrowHeads = value;
            }
        }

        public new Image Image
        {
            get { return base.Image; }
            set
            {
                currentImage = (Bitmap)value;
                originalImage = (Image)value.Clone();
                base.Image = value;
            }
        }

        public Surface()
        {
            this.MouseDown += new MouseEventHandler(SurfaceMouseDown);
            this.MouseUp += new MouseEventHandler(SurfaceMouseUp);
            this.MouseMove += new MouseEventHandler(SurfaceMouseMove);
            this.MouseDoubleClick += new MouseEventHandler(SurfaceDoubleClick);
            this.Paint += new PaintEventHandler(SurfacePaint);
        }

        private void SurfaceMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mX = e.X;
                mY = e.Y;
                mouseDown = true;
                drawingElement = null;

                if (DrawingMode == DrawingModes.Rect) // Draw rectangle
                {
                    drawingElement = new RectangleContainer(this);
                }
                else if (DrawingMode == DrawingModes.Ellipse) // Draw ellipse
                {
                    drawingElement = new EllipseContainer(this);
                }
                else if (DrawingMode == DrawingModes.Line) // Draw line
                {
                    drawingElement = new LineContainer(this);
                }
                else if (DrawingMode == DrawingModes.Text) // Draw textbox
                {
                    drawingElement = new TextContainer(this);
                }

                // check whether an existing element was clicked
                // we save mouse down element separately from selectedElements (checked on mouse up),
                // since it could be moved around before it is actually selected
                if (DrawingMode == DrawingModes.None)
                {
                    mouseDownElement = elements.ClickableElementAt(e.X, e.Y);
                }
                else
                {
                    mouseDownElement = selectedElements.ClickableElementAt(e.X, e.Y);
                }

                if (mouseDownElement == null)
                {
                    DeselectAllElements();
                }

                // if a new element has been drawn, set location and register it
                if (drawingElement != null)
                {
                    drawingElement.Left = e.X;
                    drawingElement.Top = e.Y;
                    drawingElement.Selected = true;
                    drawingElement.ForeColor = conf.Editor_ForeColor;
                    drawingElement.BackColor = conf.Editor_BackColor;
                    drawingElement.GradientColor = conf.Editor_GradientColor;
                    drawingElement.GradientType = conf.Editor_GradientType;
                    drawingElement.Thickness = conf.Editor_Thickness;
                    drawingElement.ArrowHeads = conf.Editor_ArrowHeads;
                    AddElement(drawingElement);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                DeselectAllElements();
            }
        }

        private void SurfaceMouseMove(object sender, MouseEventArgs e)
        {
            bool clickable = selectedElements.ClickableAt(e.X, e.Y);

            if (DrawingMode == DrawingModes.None && !clickable)
            {
                Cursor = Cursors.Default;
            }
            else if ((!mouseDown && clickable) || (mouseDown && mouseDownElement != null))
            {
                Cursor = Cursors.SizeAll;
            }
            else
            {
                Cursor = Cursors.Cross;
            }

            if (mouseDown)
            {
                if (mouseDownElement != null) // an element is currently dragged
                {
                    selectedElements.HideGrippers();
                    if (mouseDownElement.Selected) // dragged element has been selected before -> move all
                    {
                        selectedElements.MoveBy(e.X - mX, e.Y - mY);
                    }
                    else // dragged element is not among selected elements -> just move dragged one
                    {
                        mouseDownElement.MoveBy(e.X - mX, e.Y - mY);
                    }
                    mX = e.X; mY = e.Y;
                    Invalidate();
                }
                else if (drawingElement != null) // an element is currently drawn
                {
                    drawingElement.Width = e.X - drawingElement.Left;
                    drawingElement.Height = e.Y - drawingElement.Top;
                    Invalidate();
                }
            }
        }

        private void SurfaceMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (DrawingMode == DrawingModes.None)
                { // check whether an existing element was clicked
                    DrawableContainer element = elements.ClickableElementAt(e.X, e.Y);
                    bool shiftModifier = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                    if (element != null)
                    {
                        bool alreadySelected = selectedElements.Contains(element);
                        if (shiftModifier)
                        {
                            if (alreadySelected) DeselectElement(element);
                            else SelectElement(element);
                        }
                        else
                        {
                            if (!alreadySelected)
                            {
                                DeselectAllElements();
                                SelectElement(element);
                            }
                        }
                    }
                    else if (!shiftModifier)
                    {
                        DeselectAllElements();
                    }
                }

                if (selectedElements.Count > 0)
                {
                    selectedElements.ShowGrippers();
                    selectedElements.Selected = true;
                }

                if (drawingElement != null && mouseDownElement == null)
                {
                    if (!drawingElement.InitContent())
                    {
                        elements.Remove(drawingElement);
                        Invalidate();
                    }
                    else
                    {
                        if (Math.Abs(drawingElement.Width) < 5 && Math.Abs(drawingElement.Height) < 5)
                        {
                            drawingElement.Width = 25;
                            drawingElement.Height = 25;
                        }
                        SelectElement(drawingElement);
                        drawingElement.Selected = true;
                    }
                    drawingElement = null;
                }

                mouseDown = false;
                mouseDownElement = null;

                Invalidate();
            }
        }

        private void SurfaceDoubleClick(object sender, MouseEventArgs e)
        {
            selectedElements.OnDoubleClick();
            Invalidate();
        }

        private void SurfacePaint(object sender, PaintEventArgs e)
        {
            currentImage = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(currentImage);
            g.DrawImageUnscaled(originalImage, new Point(0, 0));
            elements.Draw(g, DrawableContainer.RenderMode.EDIT);
            e.Graphics.DrawImage(currentImage, 0, 0);
        }

        public void AddElement(DrawableContainer element)
        {
            elements.Add(element);
            Invalidate();
        }

        public void AddElements(DrawableContainerList elems)
        {
            elements.AddRange(elems);
            Invalidate();
        }

        public Bitmap GetImageForExport()
        {
            Bitmap ret = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(ret);
            g.DrawImageUnscaled(originalImage, new Point(0, 0));
            elements.Draw(g, DrawableContainer.RenderMode.EXPORT);
            g.DrawImage(ret, 0, 0);
            return ret;
        }

        public static Bitmap GetImageForExport(Image img)
        {
            Bitmap ret = new Bitmap(img.Width, img.Height);
            Graphics g = Graphics.FromImage(ret);
            g.DrawImageUnscaled(img, new Point(0, 0));
            DrawableContainerList elements = new DrawableContainerList();
            elements.Draw(g, DrawableContainer.RenderMode.EXPORT);
            g.DrawImage(ret, 0, 0);
            return ret;
        }

        public void RemoveSelectedElements()
        {
            if (selectedElements.Count > 0)
            {
                foreach (DrawableContainer element in selectedElements)
                {
                    elements.Remove(element);
                    element.Dispose();
                }
                selectedElements.Clear();
                Invalidate();
            }
        }

        public bool CutSelectedElements(BackgroundWorker worker)
        {
            if (selectedElements.Count > 0)
            {
                worker.ReportProgress(104, selectedElements);
                RemoveSelectedElements();
                return true;
            }
            return false;
        }

        public bool CopySelectedElements(BackgroundWorker worker)
        {
            if (selectedElements.Count > 0)
            {
                worker.ReportProgress(104, selectedElements);
                return true;
            }
            return false;
        }

        public void PasteElementFromClipboard()
        {
            IDataObject ido = Clipboard.GetDataObject();
            DrawableContainerList dc = null;
            if (ido.GetDataPresent(typeof(DrawableContainerList)))
            {
                dc = (DrawableContainerList)ido.GetData(typeof(DrawableContainerList));
            }
            if (dc != null)
            {
                dc.Parent = this;
                dc.MoveBy(10, 10);
                AddElements(dc);
                DeselectAllElements();
                SelectElements(dc);
            }
        }

        public void DuplicateSelectedElements()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, selectedElements);
            ms.Seek(0, 0);
            DrawableContainerList dc = (DrawableContainerList)bf.Deserialize(ms);
            dc.Parent = this;
            dc.MoveBy(10, 10);
            AddElements(dc);
            DeselectAllElements();
            SelectElements(dc);
        }

        public void DeselectElement(DrawableContainer element)
        {
            element.HideGrippers();
            element.Selected = false;
            selectedElements.Remove(element);
            MovingElementChanged(this, selectedElements);
        }

        public void DeselectAllElements()
        {
            while (selectedElements.Count > 0)
            {
                DrawableContainer element = selectedElements[0];
                element.HideGrippers();
                element.Selected = false;
                selectedElements.Remove(element);
            }
            MovingElementChanged(this, selectedElements);
        }

        public void SelectElement(DrawableContainer element)
        {
            if (selectedElements.Contains(element)) return;
            selectedElements.Add(element);
            element.ShowGrippers();
            element.Selected = true;
            MovingElementChanged(this, selectedElements);
            Invalidate();
        }

        public void SelectElements(DrawableContainerList elements)
        {
            foreach (DrawableContainer element in elements)
            {
                SelectElement(element);
            }
        }

        public void ProcessCmdKey(Keys k)
        {
            if (selectedElements.Count > 0)
            {
                int px = (k == Keys.Shift) ? 10 : 1;
                switch (k)
                {
                    case Keys.Left:
                        selectedElements.MoveBy(-1, 0);
                        break;
                    case Keys.Left | Keys.Shift:
                        selectedElements.MoveBy(-10, 0);
                        break;
                    case Keys.Up:
                        selectedElements.MoveBy(0, -1);
                        break;
                    case Keys.Up | Keys.Shift:
                        selectedElements.MoveBy(0, -10);
                        break;
                    case Keys.Right:
                        selectedElements.MoveBy(1, 0);
                        break;
                    case Keys.Right | Keys.Shift:
                        selectedElements.MoveBy(10, 0);
                        break;
                    case Keys.Down:
                        selectedElements.MoveBy(0, 1);
                        break;
                    case Keys.Down | Keys.Shift:
                        selectedElements.MoveBy(0, 10);
                        break;
                    case Keys.PageUp:
                        elements.PullElementsUp(selectedElements);
                        break;
                    case Keys.PageDown:
                        elements.PushElementsDown(selectedElements);
                        break;
                    case Keys.Home:
                        elements.PullElementsToTop(selectedElements);
                        break;
                    case Keys.End:
                        elements.PushElementsToBottom(selectedElements);
                        break;
                    default:
                        return;
                }
                Invalidate();
            }
        }

        /// <summary>
        /// pulls selected elements up one level in hierarchy
        /// </summary>
        public void PullElementsUp()
        {
            elements.PullElementsUp(selectedElements);
            Invalidate();
        }

        /// <summary>
        /// pushes selected elements up to top in hierarchy
        /// </summary>
        public void PullElementsToTop()
        {
            elements.PullElementsToTop(selectedElements);
            Invalidate();
        }

        /// <summary>
        /// pushes selected elements down one level in hierarchy
        /// </summary>
        public void PushElementsDown()
        {
            elements.PushElementsDown(selectedElements);
            Invalidate();
        }

        /// <summary>
        /// pushes selected elements down to bottom in hierarchy
        /// </summary>
        public void PushElementsToBottom()
        {
            elements.PushElementsToBottom(selectedElements);
            Invalidate();
        }

        /// <summary>
        /// indicates whether the selected elements could be pulled up in hierarchy
        /// </summary>
        /// <returns>true if selected elements could be pulled up, false otherwise</returns>
        public bool CanPullSelectionUp()
        {
            return elements.CanPullUp(selectedElements);
        }

        /// <summary>
        /// indicates whether the selected elements could be pushed down in hierarchy
        /// </summary>
        /// <returns>true if selected elements could be pushed down, false otherwise</returns>
        public bool CanPushSelectionDown()
        {
            return elements.CanPushDown(selectedElements);
        }

        public bool IsEdited()
        {
            return elements.Count > 0;
        }
    }
}