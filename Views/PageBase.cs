using Core;
using Xamarin.Forms;

namespace SampleApplication.Views
{
    public enum PageOrientation
    {
        Landscape = 0,
        Portrait = 1,
    }

    public class PageBase : ContentPage, IView
    {
        protected const int SizeNotSet = -1;
        protected double _height;
        protected double _width;

        public PageBase() : base()
        {
            Init();
        }

        public PageOrientation PageOrientation
        {
            get; set;
        }

        public IViewModel ViewModel { get; set; }

        protected virtual void OnPageOrientationUpdated()
        {
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            var oldWidth = _width;

            base.OnSizeAllocated(width, height);
            if (Equals(_width, width) && Equals(_height, height)) return;

            _width = width;
            _height = height;

            // ignore if the previous height was size not yet set
            if (Equals(oldWidth, SizeNotSet)) return;

            // Has the device been rotated ?
            if (!Equals(width, oldWidth))
            {
                PageOrientation = width < height ? PageOrientation.Portrait : PageOrientation.Landscape;
                OnPageOrientationUpdated();
            }
        }

        private void Init()
        {
            _width = this.Width;
            _height = this.Height;
        }
    }
}