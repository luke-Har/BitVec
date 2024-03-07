using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace BitVecc
{
    public readonly ref struct BitVec
    {
        readonly Span<bool> _bits;

        /// <summary>
        /// Creates a BitVec initialised to 0 with length of size
        /// </summary>
        /// <param name="size"></param>
        public BitVec(uint size) 
        {
            _bits = new bool[size];
        }

        /// <summary>
        /// Copies Span into new BitVec instance
        /// </summary>
        /// <param name="bits"></param>
        public BitVec(Span<bool> bits) 
        {
            _bits = bits;
        }

        /// <summary>
        /// Copies entered BitVec into a new instance
        /// </summary>
        /// <param name="bitvec"></param>
        public BitVec(BitVec bitvec)
        {
            _bits = bitvec._bits;
        }

        public bool this[int index]
        {
            get => _bits[index];
        }

        /// <summary>
        /// Performs bitwise 'AND' operation
        /// </summary>
        /// <param name="bitvec"></param>
        /// <returns>New instance containing result</returns>
        public BitVec And(BitVec bitvec)
        {
            Span<bool> newBits = new bool[_bits.Length];
            ref var firstElementThis = ref MemoryMarshal.GetReference(_bits);
            ref var firstElementParam = ref MemoryMarshal.GetReference(bitvec._bits);
            ref var firstElementNew = ref MemoryMarshal.GetReference(newBits);

            for (int i = 0; i < _bits.Length; i++)
            {
                var elementThis = Unsafe.Add(ref firstElementThis, i);
                var elementParam = Unsafe.Add(ref firstElementParam, i);
                Unsafe.Add(ref firstElementNew, i) = elementThis & elementParam;
            }
            return new BitVec(newBits);
        }

        /// <summary>
        /// Performs bitwise 'OR' operation
        /// </summary>
        /// <param name="bitvec"></param>
        /// <returns>New instance containing result</returns>
        public BitVec Or(BitVec bitvec)
        {
            Span<bool> newBits = new bool[_bits.Length];
            ref var firstElementThis = ref MemoryMarshal.GetReference(_bits);
            ref var firstElementParam = ref MemoryMarshal.GetReference(bitvec._bits);
            ref var firstElementNew = ref MemoryMarshal.GetReference(newBits);

            for (int i = 0; i < _bits.Length; i++)
            {
                var elementThis = Unsafe.Add(ref firstElementThis, i);
                var elementParam = Unsafe.Add(ref firstElementParam, i);
                Unsafe.Add(ref firstElementNew, i) = elementThis | elementParam;
            }
            return new BitVec(newBits);
        }

        /// <summary>
        /// Performs bitwise 'XOR' operation
        /// </summary>
        /// <param name="bitvec"></param>
        /// <returns>New instance containing result</returns>
        public BitVec Xor(BitVec bitvec)
        {
            Span<bool> newBits = new bool[_bits.Length];
            ref var firstElementThis = ref MemoryMarshal.GetReference(_bits);
            ref var firstElementParam = ref MemoryMarshal.GetReference(bitvec._bits);
            ref var firstElementNew = ref MemoryMarshal.GetReference(newBits);

            for (int i = 0; i < _bits.Length; i++)
            {
                var elementThis = Unsafe.Add(ref firstElementThis, i);
                var elementParam = Unsafe.Add(ref firstElementParam, i);
                Unsafe.Add(ref firstElementNew, i) = elementThis ^ elementParam;
            }
            return new BitVec(newBits);
        }

        /// <summary>
        /// Performs bitwise negation
        /// </summary>
        /// <param name="bitvec"></param>
        /// <returns>New instance containing result</returns>
        public BitVec Negate()
        {
            Span<bool> newBits = new bool[_bits.Length];
            ref var firstElementThis = ref MemoryMarshal.GetReference(_bits);
            ref var firstElementNew = ref MemoryMarshal.GetReference(newBits);

            for (int i = 0; i < _bits.Length; i++)
            {
                var elementThis = Unsafe.Add(ref firstElementThis, i);
                Unsafe.Add(ref firstElementNew, i) = !elementThis;
            }
            return new BitVec(newBits);
        }

        public BitVec LShift(uint amount) 
        {
            Span<bool> newBits = new bool[_bits.Length];
            ref var firstElement = ref MemoryMarshal.GetReference(_bits);
            ref var firstElementNew = ref MemoryMarshal.GetReference(newBits);
            for (uint i = 0; i < _bits.Length; i++)
            {
                bool currentItem = Unsafe.Add(ref firstElement, i);
                bool withinBounds = i >= amount;
                bool value = withinBounds && currentItem;
                Unsafe.Add(ref firstElementNew, withinBounds ? i - amount : i) = value;
            }
            return new BitVec(newBits);
        }

        public BitVec RShift(uint amount)
        {
            Span<bool> newBits = new bool[_bits.Length];
            ref var firstElement = ref MemoryMarshal.GetReference(_bits);
            ref var firstElementNew = ref MemoryMarshal.GetReference(newBits);
            for (uint i = (uint)(_bits.Length); i-- > 0; )
            {
                bool currentItem = Unsafe.Add(ref firstElement, i);
                bool withinBounds = i + amount < _bits.Length;
                bool value = withinBounds && currentItem;
                Unsafe.Add(ref firstElementNew, withinBounds ? i + amount : i) = value;
            }
            return new BitVec(newBits);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>BitVec parsed into 0s and 1s</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            ref var firstElement = ref MemoryMarshal.GetReference(_bits);
            for (int i = 0; i < _bits.Length; i++)
            {
                sb.Append(Unsafe.Add(ref firstElement, i) ? '1' : '0');
            }
            return sb.ToString();
        }
    }
}
