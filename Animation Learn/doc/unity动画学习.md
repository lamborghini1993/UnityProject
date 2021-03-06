# 动画分类

## 1.普通动画

一般添加动画有几种方式：

1. 增加属性来添加（Add Property）

   - 选择你需要生成动画改变的属性

      ![1530151862](1530151862.png)

   - 比如选择了Position，会自动在0秒（第一帧）和第1秒（第60帧）生成当前一样属性的关键帧。

     ![1530151912](1530151912.png)

   - 将关键帧进度条拖到需要添加关键帧的位置，修改属性数值即可添加。

      ![1530152013](1530152013.png)


2. 右键添加关键帧![20180628_111312](20180628_111312.png)

3. 录制添加关键帧

   点击录制时，拖动到需要添加关键帧的位置，然后再Inspector面板直接修改数据即可

   ![20180628_111606](20180628_111606.png)

运行查看效果如图：

![img](11-19-42-6-28-20534.gif)

- 关于曲线编辑

  ![1530156838834](1530156838834.png)

  - 点击Curves即可进行曲线编辑，可看到默认是按照平滑的曲线来进行的。

  - 每一条颜色的先代码左侧对应的属性（因为之前属性设置一样，所以三天线是重合的）。

  - 可以针对每个关键帧做曲线设置，比如：设置为直线，或者自定义拖拉为曲线，为了直观看到变化这里演示只改变Z轴位置。

    ​	![img](11-43-28-6-28-25191.gif)

  - 可以看到运动轨迹先像Z轴正方向运行迅速移动，后快速向负方向移动然后稍微慢速的移到原点。

    ![img](11-45-8-6-28-25517.gif)

  

## 2.UGUI的按钮动画（四个状态）

- 添加一个Canvas，然后在里面添加一个Button，选择Button（Script）里面的Transition为Animation

  ![1530156294193](1530156294193.png)

- 会出现四个状态，点击应用选择路径保存Animation

  ![1530156347286](1530156347286.png)

  - 四个状态分别为：正常、高亮（鼠标移动到按钮上没点击）、点击按钮、不可用状态

  - 打开动画模式窗口，会发现已有四个模式存在。

    ​![1530156530154](1530156530154.png)

  - 进行和一种普通动画一样的编辑即可。

## 3.2D游戏精灵动画

- ![fly201806281347](fly201806281347.gif)
- 选择多个精灵文件，直接拖动到Hierarchy窗口，保存即可创建动画。
- 默认一帧播放一张图片。
- 操作方式和（1）中一样。