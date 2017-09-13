using Xamarin.Forms;

namespace SampleApplication.Views
{
    public enum PageOrientation
    {
        Landscape = 0,
        Portrait = 1,
    }

    public class PageBase : ContentPage
    {
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

        protected virtual void OnPageOrientationUpdated()
        {
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            var oldWidth = _width;
            const double sizenotallocated = -1;

            base.OnSizeAllocated(width, height);
            if (Equals(_width, width) && Equals(_height, height)) return;

            _width = width;
            _height = height;

            // ignore if the previous height was size unallocated
            if (Equals(oldWidth, sizenotallocated)) return;

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