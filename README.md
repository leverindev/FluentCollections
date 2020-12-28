# FluentCollections

![Build](https://github.com/leverindev/FluentCollections/workflows/dotnetcorebuild/badge.svg)

### Introduction
FluentCollections is a small class library with useful collections.

### Collections

#### ModifiableList

Sometimes it's convenient to modify the list during iteration. However, in this case, we get an exception "Collection was modified". There is a solution: make `ToList()` on the original collection, however, it produces a memory allocation.

`ModifiableList` allows to modify the collection during iteration, but the changes are applied only after calling `Save` method.

#### PriorityQueue

Binary heap is a common way of implementing priority queue. For more information follow the [link](https://en.wikipedia.org/wiki/Binary_heap)

### NuGet

[This package is available on the nuget.org](https://www.nuget.org/packages/FluentCollections_leverindev)
