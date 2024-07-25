<div align="center">
    <h1>BitVec</h1>
</div>


Simple, immutable and lightweight intermediary for BitArray.

For when you wish BitArray was more like LINQ.

It can be used as a lightweight intermediary between BitArray instances, allowing for chaining of operations and very little GC pressure.
```csharp
BitVec mask = new BitVec(new bool[] {0,0,1,1,1,1});
BitArray bitArr = new BitArray(new bool[] {1,0,0,1,1,0});
BitArray newAndDifferentBitArr = new BitVec(bitArr).RShift(3).Xor(mask).Negate().ToBitArray();
```

It also has operator overloads.
```csharp
BitVec val1 = new BitVec(new bool[] {0,0,1,1,1,1});
BitVec val2 = new BitVec(new bool[] {1,1,1,1,0,0});
BitVec result = val1 ^ val2; //output: 110011
```
