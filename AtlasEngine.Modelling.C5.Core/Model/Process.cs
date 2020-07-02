namespace AtlasEngine.Modelling.C5
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class Process : StaticStructureElement, IEquatable<Process>
    {
        /// <summary>
        /// The location of this person.
        /// </summary>
        [DataMember(Name = "location", EmitDefaultValue = true)]
        public Location Location { get; set; }

        public override string CanonicalName
        {
            get
            {
                return CanonicalNameSeparator + FormatForCanonicalName(Name);
            }
        }

        public override Element Parent
        {
            get
            {
                return null;
            }

            set
            {
            }
        }

        internal Process()
        {
        }

        public override List<string> GetRequiredTags()
        {
            return new List<string>
            {
                AtlasEngine.Modelling.C5.Tags.Element,
                AtlasEngine.Modelling.C5.Tags.Process
            };
        }

        public new Relationship Delivers(Process destination, string description)
        {
            throw new InvalidOperationException();
        }

        public new Relationship Delivers(Process destination, string description, string technology)
        {
            throw new InvalidOperationException();
        }

        public new Relationship Delivers(Process destination, string description, string technology, InteractionStyle interactionStyle)
        {
            throw new InvalidOperationException();
        }

        public Relationship InteractsWith(Process destination, string description)
        {
            return Model.AddRelationship(this, destination, description);
        }

        public Relationship InteractsWith(Process destination, string description, string technology)
        {
            return Model.AddRelationship(this, destination, description, technology);
        }

        public Relationship InteractsWith(Process destination, string description, string technology, InteractionStyle interactionStyle)
        {
            return Model.AddRelationship(this, destination, description, technology, interactionStyle);
        }

        public bool Equals(Process process)
        {
            return this.Equals(process as Element);
        }
    }
}
