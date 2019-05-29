using System;
using System.Collections.Generic;
using System.Text;

namespace GISLib
{
    /// <summary>
    /// 公共定义类
    /// </summary>
    public class PublicDim
    {
        //行政区地图图层索引值
        public static int ZoneTblIndex = 2;//县区
        public static int ProvinceTblIndex = 2; //省
        public static int CityTblIndex = 2;//市
        //兴趣点地图图层索引值
        public static int PointTblIndex = 0;
        //道路地图图层索引值
        public static int RoadTblIndex = 1;

        //行政区地图名称字段
        public static string ZoneTblColumn = "NAME";
        public static string ProvinceTblColumn = "NAME";
        public static string CityTblColumn = "NAME";
        //兴趣点地图名称字段
        public static string PointTblColumn = "NAME_1";
        //道路地图名称字段
        public static string RoadTblColumn = "NAME";
    }
}
