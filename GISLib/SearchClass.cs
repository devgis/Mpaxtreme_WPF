using System;
using System.Collections.Generic;
using System.Text;
using MapInfo.Data;
using MapInfo.Mapping;
using System.Reflection;
using System.IO;
using MapInfo.Windows.Controls;
using System.Windows.Forms;
using MapInfo.Geometry;

namespace GISLib
{
    /// <summary>
    /// 查询类
    /// </summary>
    public class SearchClass
    {
        #region Table实例
        Map map;
        MapControl mControl;
        CoordSys cs;
        #endregion

        #region 单例实例
        private static SearchClass instance;
        /// <summary>
        /// 单例实例
        /// </summary>
        public static SearchClass Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SearchClass();
                    instance.InitMemMap();
                }
                return instance;
            }
        }
        #endregion

        #region 共有方法
        /// <summary>
        /// 调用此方法创建对象不做任何其他操作
        /// </summary>
        public void Init()
        { }

        /// <summary>
        /// 搜索附近信息 
        /// </summary>
        /// <param name="x">经度</param>
        /// <param name="y">纬度</param>
        /// <param name="layerIndex">图层顺序号</param>
        /// <param name="colname">列名</param>
        /// <param name="distance">默认最大距离</param>
        /// <returns></returns>
        public String SearchNearInfo(double x, double y,int layerIndex,string colname, double distance = 100)
        {
            Point pSearchPoint = new Point(cs, x, y);
            String RoadName = string.Empty;
            SearchInfo si = MapInfo.Data.SearchInfoFactory.SearchNearest(pSearchPoint, new Distance(distance, DistanceUnit.Meter));
            si.QueryDefinition.Columns = null;
            Table tmpTable = (map.Layers[layerIndex] as FeatureLayer).Table;
            IResultSetFeatureCollection ifs = MapInfo.Engine.Session.Current.Catalog.Search(tmpTable, si);
            if (ifs.Count > 0)
            {
                RoadName = ifs[0][colname].ToString();
            }
            return RoadName;

        }

        /// <summary>
        /// 根据经纬度查询周边信息点
        /// </summary>
        /// <param name="x">经度</param>
        /// <param name="y">纬度</param>
        /// <param name="searchzone">是否搜索行政区域</param>
        /// <param name="searchroad">是否搜索道路</param>
        /// <param name="searchpoint">是否搜索兴趣点</param>
        /// <param name="distance">搜索距离 默认100M</param>
        /// <returns>返回周边兴趣点</returns>
        public SearchResult Search(double x, double y, bool searchzone = true, bool searchroad = true, bool searchpoint = true,double distance=100)
        {
            SearchResult result = new SearchResult();
            if (searchzone)
            {
                result.ZoneName = SearchNearInfo(x, y,PublicDim.ProvinceTblIndex,PublicDim.ProvinceTblColumn,distance)+ SearchNearInfo(x, y, PublicDim.CityTblIndex, PublicDim.CityTblColumn, distance)+ SearchNearInfo(x, y, PublicDim.ZoneTblIndex, PublicDim.ZoneTblColumn, distance);
            }
            if (searchroad)
            {
                result.RoadName = SearchNearInfo(x, y, PublicDim.RoadTblIndex, PublicDim.RoadTblColumn, distance);
            }
            if (searchpoint)
            {
                result.PointName = SearchNearInfo(x, y, PublicDim.PointTblIndex, PublicDim.PointTblColumn, distance);
            }
            return result;
        }

        #endregion


        #region 私有方法
        /// <summary>
        /// 初始化地图数据到内存
        /// </summary>
        public void InitMemMap()
        {
            mControl=new MapControl();
            string path = Application.StartupPath; //Assembly.GetExecutingAssembly().Location;
            String TablePath = Path.Combine(path, "Map");
            String MWSPath=Path.Combine(TablePath,"map.mws");
            MapInfo.Mapping.MapTableLoader tLoader = new MapTableLoader();
            map = mControl.Map;
            MapWorkSpaceLoader mwsLoader = new MapWorkSpaceLoader(MWSPath);
            map.Load(mwsLoader);
            cs = map.GetDisplayCoordSys();
        }
        #endregion
    }
}
