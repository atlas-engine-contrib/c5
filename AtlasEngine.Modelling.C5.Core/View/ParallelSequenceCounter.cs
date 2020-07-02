namespace AtlasEngine.Modelling.C5
{

    internal class ParallelSequenceCounter : SequenceCounter
    {

        internal ParallelSequenceCounter(SequenceCounter parent) : base(parent)
        {
            Sequence = Parent.Sequence;
        }

    }

}