using System.Runtime.Serialization;

namespace AtlasEngine.Modelling.C5
{

    /// <summary>
    /// The X, Y coordinate of a bend in a line.
    /// </summary>
    [DataContract]
    public sealed class Vertex
    {
        public Vertex()
        {
        }

        public Vertex(int? x, int? y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// The horizontal position of the vertex when rendered.
        /// </summary>
        [DataMember(Name="x", EmitDefaultValue=false)]
        public int? X { get; set; }


        /// <summary>
        /// The vertical position of the vertex when rendered.
        /// </summary>
        [DataMember(Name="y", EmitDefaultValue=false)]
        public int? Y { get; set; }

    }
}
