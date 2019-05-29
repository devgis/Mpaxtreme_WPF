using MapInfo.Mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GISLib
{
    public class MapControl:MapInfo.Windows.Controls.MapControl
    {
        public void ShowMap()
        {
            string path = Application.StartupPath; //Assembly.GetExecutingAssembly().Location;
            String TablePath = Path.Combine(path, "Map");
            String MWSPath = Path.Combine(TablePath, "map.mws");
            MapInfo.Mapping.MapTableLoader tLoader = new MapTableLoader();
            MapWorkSpaceLoader mwsLoader = new MapWorkSpaceLoader(MWSPath);
            this.Map.Load(mwsLoader);
        }
    }
}
