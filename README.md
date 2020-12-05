# FluentCollections

![Build](https://github.com/leverindev/FluentCollections/workflows/dotnetcorebuild/badge.svg)

### Introduction
FluentCollections is a small class library with useful collections.

### Collections

#### ModifiableList

Sometimes it's convenient to modify the list during iteration. However, in this case, we get an exception "Collection was modified". There is a solution: make `ToList()` on the original collection, however, it produces a memory allocation.

`ModifiableList` allows to modify the collection during iteration, but the changes are applied only after calling `Save` method.

#### BinaryHeap

`BinaryHeap` is a common way of implementing priority queue.
