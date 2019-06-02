尝试了一些将Billboard合批绘制的方法，包含原始方案共产生了4个场景，其配置和截图如下：

| index | 使用方案 | 配置选项 | 运行截图 |
| --- | --- | --- | --- |
| 1 | 对照组，不合批 | ![config_01](Screenshots/config_01.png) | ![scene_01](Screenshots/scene_01.gif) |
| 2 | 使用GPUInstancing | ![config_02](Screenshots/config_02.png) | ![scene_02](Screenshots/scene_02.gif) |
| 3 | 静态合批 | ![config_03](Screenshots/config_03.png) | ![scene_03](Screenshots/scene_03.gif) |
| 4 | 合并网格 | ![config_04](Screenshots/config_04.png) | ![scene_04](Screenshots/scene_04.gif) |

详情请运行各场景，并参考`BatchingConfig`脚本。
