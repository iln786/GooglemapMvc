using System;
using System.Collections.Generic;
using System.Linq;

namespace Jmelosegui.Mvc.Googlemap.Overlays
{
    internal class HeatmapLayerSerializer : ISerializer
    {
        private readonly HeatmapLayer layer;

        public HeatmapLayerSerializer(HeatmapLayer layer)
        {
            if (layer == null) throw new ArgumentNullException("layer");
            this.layer = layer;
        }

        public IDictionary<string, object> Serialize()
        {
            List<string> gradientCollection = layer.Gradient.Select(color => String.Format("rgba({0}, {1}, {2}, {3})", color.R, color.G, color.B, color.A)).ToList();


            IDictionary<string, object> layerDictionary = new Dictionary<string, object>();
            FluentDictionary.For(layerDictionary)
                .Add("dissipating", layer.Dissipating, () => layer.Dissipating)
                .Add("maxIntensity", layer.MaxIntensity, () => layer.MaxIntensity > 0)
                .Add("opacity", layer.Opacity, () => layer.Opacity < 1)
                .Add("radius", layer.Radius)
                .Add("gradient", gradientCollection, () => layer.Gradient.Any())
                .Add("data", layer.Data, () => layer.Data.Any());

            IDictionary<string, object> result = new Dictionary<string, object>();
            result["heatLayer"] = layerDictionary;
            return result;
        }
    }
}