using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class ButtonDto
    {
        string name;
        string label;
        
        Action<string> callback;

        Boolean enabled;

        public string Name
        {
            get { return name; }
            set { name = value;  }
        }

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public Action<string> Callback
        {
            get { return callback; }
            set { callback = value; }
        }

        public Boolean Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

    }
}
