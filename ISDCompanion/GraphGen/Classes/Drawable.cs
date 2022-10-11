using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraphGen.Classes
{
    public class Drawable
    {

        private AbsoluteLayout _hiddenView = null;
        public GraphGen _GraphGen = null;
        public GraphGen GraphGen { get { return _GraphGen; } }

        protected AbsoluteLayout _View
        {
            get { return _hiddenView; }
            set
            {
                if (value != null)
                {
                    _hiddenView = value;
                    _hiddenView.GestureRecognizers.Add(TapGestureRecognizer);
                }
            }
        }
        public AbsoluteLayout View { get { return _View; } }

        private readonly TapGestureRecognizer TapGestureRecognizer = new TapGestureRecognizer();

        public Position Position { get; set; }


        public Drawable(GraphGen graphGen)
        {
            _GraphGen = graphGen;
            Position = new Position();
            TapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            OnThresholdReached(new OnClickEventArgs
            {
                OrgSender = sender,
                View = _View,
            });
        }

        protected virtual void OnThresholdReached(OnClickEventArgs e)
        {
            EventHandler<OnClickEventArgs> handler = OnClickEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<OnClickEventArgs> OnClickEvent;

    }
    public class OnClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public object OrgSender { get; set; }
    }



}
