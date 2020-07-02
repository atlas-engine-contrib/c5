namespace AtlasEngine.Modelling.C5
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class EventElement : StaticStructureElement, IEquatable<EventElement>
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

        internal EventElement()
        {
        }

        public override List<string> GetRequiredTags()
        {
            return new List<string>
            {
                AtlasEngine.Modelling.C5.Tags.Element,
                AtlasEngine.Modelling.C5.Tags.EventElement
            };
        }

        public new Relationship Delivers(EventElement destination, string description)
        {
            throw new InvalidOperationException();
        }

        public new Relationship Delivers(EventElement destination, string description, string technology)
        {
            throw new InvalidOperationException();
        }

        public new Relationship Delivers(EventElement destination, string description, string technology, InteractionStyle interactionStyle)
        {
            throw new InvalidOperationException();
        }

        public Relationship InteractsWith(EventElement destination, string description)
        {
            return Model.AddRelationship(this, destination, description);
        }

        public Relationship InteractsWith(EventElement destination, string description, string technology)
        {
            return Model.AddRelationship(this, destination, description, technology);
        }

        public Relationship InteractsWith(EventElement destination, string description, string technology, InteractionStyle interactionStyle)
        {
            return Model.AddRelationship(this, destination, description, technology, interactionStyle);
        }

        public bool Equals(EventElement item)
        {
            return this.Equals(item as Element);
        }
    }
}
