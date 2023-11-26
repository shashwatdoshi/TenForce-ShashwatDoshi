using System;
using System.Linq;

using Newtonsoft.Json;

using Test_Taste_Console_Application.Domain.DataTransferObjects.JsonObjects;

namespace Test_Taste_Console_Application.Domain.DataTransferObjects
{
    //The converter is needed for this DTO. Because of the converter, all the properties need to get the JsonProperty annotation. 
    [Newtonsoft.Json.JsonConverter(typeof(JsonPathConverter))]
    public class MoonDto
    {
        [JsonProperty("id")] public string Id { get; set; }

        //The property moon is used to set the Id property. 
        [JsonProperty("moon")]
        public string Moon
        {
            get => Id;
            set => Id = value;
        }

        //The path of the specific moon
        [JsonProperty("rel")] public string Rel { get; set; }
        public string URLId { get => Rel.Split('/').Last(); }

        //The path to the nested property is created by using a dot. 
        [JsonProperty("mass.massValue")] public float MassValue { get; set; } = 0.0f;
        [JsonProperty("mass.massExponent")] public float MassExponent { get; set; } = 0.0f;
        [JsonProperty("meanRadius")]
        public float MeanRadius { get; set; }

        [JsonProperty("gravity")]
        public float Gravity /*{  get; set; }*/
        {
            get
            {
                // I have re-calculate Gravity again as there is gravity 0 in many Moons.
                // If we don't want to re-calculate it then above mentioned just get; set;
                // should be uncommented and this code should commented.

                // We can get Gravity by g = G * M / R * R

                // G is The Gravitational Constant G = 6.6743 × 10−11 Nm2kg−2 (kg−1m3s−2)
                // M is Mass
                // R is Radius in Meters

                var G = 6.6743 * Math.Pow(10, -11);
                var M = MassValue * Math.Pow(10, MassExponent);
                var R = MeanRadius * 1000;

                return (float)(G * M / Math.Pow(R, 2));
            }
        }
    }
}
