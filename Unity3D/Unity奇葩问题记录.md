# Unity 奇葩问题记录

1. The AssetBundle 'timeline.cfg' can't be loaded because another AssetBundle with the same files is already loaded.
这个问题，是AB资源引起的， A和B两个资源有引用关系，B引用A，A更新后，build A的AB资源，没有同步生成B的AB资源。 应该清理原因了AB资源，重新build B，根据引用，build B的时候会同时build A的资源

