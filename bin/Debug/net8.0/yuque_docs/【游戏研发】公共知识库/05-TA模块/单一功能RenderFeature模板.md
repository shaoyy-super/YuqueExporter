完整说明导航：[https://www.bilibili.com/read/cv29070934/?spm_id_from=333.999.0.0](https://www.bilibili.com/read/cv29070934/?spm_id_from=333.999.0.0)



前置：

### 抽象基类：
![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719464916325-9092387c-ed21-42ea-9b36-a5b75c24d653.png)

新建抽象基类，继承VolumeComponent, IPostProcessComponent, IDisposable

定义插入节点与插入顺序，以及一些函数供子类自行完成逻辑，图中为Setup(), OnCameraSetup()和Render()，实际操作中可根据情况再自行添加函数



### RenderFeature:
```plain
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using Showcase.ShowcaseURP;


namespace Showcase.ShowcaseURP
{
    public class ShowcaseRendererFeature : ScriptableRendererFeature
    {
        // 获取后处理基类列表
        private List<ShowcaseProcessing> mShowcaseProcessings;

        // 注入Pass
        private ShowcaseProcessPass m_AfterOpaquePass;
        private ShowcaseProcessPass m_AfterSkyboxPass;
        private ShowcaseProcessPass m_BeforePostProcessPass;
        private ShowcaseProcessPass m_AfterPostProcessPass;



        public override void Create()
        {

            var stack = VolumeManager.instance.stack;
            // 获取 所有继承自 BasicPostProcessing 类型的Volume组件 并增加到列表中
            mShowcaseProcessings = VolumeManager.instance.baseComponentTypeArray
            .Where(t => t.IsSubclassOf(typeof(ShowcaseProcessing)))
            .Select(t => stack.GetComponent(t) as ShowcaseProcessing)
            .ToList();


            // 设置 AfterOpaque 的后处理效果
            var afterOpaqueCPPs = mShowcaseProcessings
                .Where(c => c.InjectionPoint == BasicInjectionPoint.AfterOpauqe)
                .OrderBy(c => c.OrderInInjectionPoint)     // 筛选出 效果进行排序
                .ToList();

            // afterOpaqueCPPs 储存所有 AfterOpauqe 类型排序后的新列表
            m_AfterOpaquePass = new ShowcaseProcessPass("不透明物体之后", afterOpaqueCPPs);
            // 对应时机
            m_AfterOpaquePass.renderPassEvent = RenderPassEvent.AfterRenderingOpaques;


            // 筛选出设置为 AfterSkybox 的后处理效果
            var afterSkyboxCPPs = mShowcaseProcessings
                .Where(c => c.InjectionPoint == BasicInjectionPoint.AfterSkybox)
                .OrderBy(c => c.OrderInInjectionPoint)     // 筛选出 效果进行排序
                .ToList();

            m_AfterSkyboxPass = new ShowcaseProcessPass(" 天空盒之后 ", afterSkyboxCPPs);
            // 对应时机
            m_AfterSkyboxPass.renderPassEvent = RenderPassEvent.AfterRenderingSkybox;


            // 筛选出设置为 BeforePostProcess 的后处理效果
            var beforePostProcessingCPPs = mShowcaseProcessings
                .Where(c => c.InjectionPoint == BasicInjectionPoint.BeforePostProcess)
                .OrderBy(c => c.OrderInInjectionPoint)
                .ToList();

            m_BeforePostProcessPass = new ShowcaseProcessPass(" 后处理之后 ", beforePostProcessingCPPs);
            // 对应时机
            m_BeforePostProcessPass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;


            // 筛选出设置为 AfterPostProcess 的后处理效果
            var afterPostProcessCPPs = mShowcaseProcessings
                .Where(c => c.InjectionPoint == BasicInjectionPoint.AfterPostProcess)
                .OrderBy(c => c.OrderInInjectionPoint)
                .ToList();

            m_AfterPostProcessPass = new ShowcaseProcessPass(" 渲染最后阶段 ", afterPostProcessCPPs);
            // 对应时机
            m_AfterPostProcessPass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;

        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {

            // 当前摄像机是否开启后处理
            if (renderingData.cameraData.postProcessEnabled)
            {
                // 并且将pass列表加到renderer中
                if (m_AfterOpaquePass.SetupPostProcessing())
                {
                    m_AfterOpaquePass.ConfigureInput(ScriptableRenderPassInput.Color);
                    renderer.EnqueuePass(m_AfterOpaquePass);
                }

                if (m_AfterSkyboxPass.SetupPostProcessing())
                {
                    m_AfterSkyboxPass.ConfigureInput(ScriptableRenderPassInput.Color);
                    renderer.EnqueuePass(m_AfterSkyboxPass);
                }

                if (m_BeforePostProcessPass.SetupPostProcessing())
                {
                    m_BeforePostProcessPass.ConfigureInput(ScriptableRenderPassInput.Color);
                    renderer.EnqueuePass(m_BeforePostProcessPass);
                }

                if (m_AfterPostProcessPass.SetupPostProcessing())
                {
                    m_AfterPostProcessPass.ConfigureInput(ScriptableRenderPassInput.Color);
                    renderer.EnqueuePass(m_AfterPostProcessPass);
                }
            }

        }

        //  释放资源
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);


            m_AfterSkyboxPass.Dispose();
            m_BeforePostProcessPass.Dispose();
            m_AfterPostProcessPass.Dispose();

            if (mShowcaseProcessings != null)
            {
                foreach (var item in mShowcaseProcessings)
                {
                    item.Dispose();
                }
            }
        }


    }

}
```



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719465472076-96933449-dd2c-4dda-b13e-7410c74b0cda.png)

通过VolumeManager获取所有继承了抽象基类的组件，加至列表

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719465582056-5c029982-cc39-45cf-957f-8dfa57ee930a.png)

根据设置的插入时机（BasicInjectionPoint）以及插入排序进行分类排序



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719465820749-0e61ca50-a24a-4678-9430-ad0242b6a8fb.png)

![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719465838655-90dc302f-3844-424c-86b3-f79390820647.png)

按类型筛选激活的后处理效果，创建对应Pass，加入管线



![](https://cdn.nlark.com/yuque/0/2024/png/45354151/1719465966122-ead159d7-76e6-4f70-9476-a42e69095364.png)

释放资源



### RenderPass:
```plain
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

namespace Showcase.ShowcaseURP
{
    public class ShowcaseProcessPass : ScriptableRenderPass
    {
        // 获取后处理基类列表
        private List<ShowcaseProcessing> mShowcaseProcessings;
        private List<int> mShowcaseProcessingIndex;         // 存储当前激活的自定义后处理效果的索引

        // 声明RT
        private RTHandle mSourceRT;
        private RTHandle mDesRT;
        private RTHandle mTempRT0;
        private RTHandle mTempRT1;
        private string mTempRT0Name => "_TemporaryRenderTexture0";
        private string mTempRT1Name => "_IntermediateRenderTarget1";


        // 性能分析的标签
        private string m_ProfilerTag; // 确保这样的声明存在
        private List<ProfilingSampler> m_ProfilingSamplers;   // 用于监控每个自定义后处理效果的性能


        // 相机初始化时执行
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            var descriptor = renderingData.cameraData.cameraTargetDescriptor;
            descriptor.msaaSamples = 1;
            descriptor.depthBufferBits = 0;

            // 分配临时纹理
            RenderingUtils.ReAllocateIfNeeded(ref mTempRT0, descriptor, name: mTempRT0Name);
            RenderingUtils.ReAllocateIfNeeded(ref mTempRT1, descriptor, name: mTempRT1Name);

            foreach (var i in mShowcaseProcessingIndex)
            {
                mShowcaseProcessings[i].OnCameraSetup(cmd, ref renderingData);
            }
        }

        // 相机清除时执行
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            mDesRT = null;
            mSourceRT = null;
        }



        // 确定哪些后处理效果是激活的，通过调用它们的 Setup 方法和检查它们的激活状态 (IsActive())
        public bool SetupPostProcessing()
        {
            mShowcaseProcessingIndex.Clear();

            for (int i = 0; i < mShowcaseProcessings.Count; i++)
            {
                mShowcaseProcessings[i].Setup();

                if (mShowcaseProcessings[i].IsActive())
                {
                    mShowcaseProcessingIndex.Add(i);
                }
            }
            return mShowcaseProcessingIndex.Count != 0;

        }

        //
        public ShowcaseProcessPass(string ProfilerTag, List<ShowcaseProcessing> ShowcaseProcessing)
        {
            m_ProfilerTag = ProfilerTag;
            mShowcaseProcessings = ShowcaseProcessing;

            mShowcaseProcessingIndex = new List<int>(ShowcaseProcessing.Count);                      // 初始化后处理效果索引列表
            m_ProfilingSamplers = ShowcaseProcessing.Select(c => new ProfilingSampler(c.ToString())).ToList();   // 性能采样器列表

        }

        // 执行逻辑
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (!SetupPostProcessing()) return;

            //初始化 commandbuffer
            var cmd = CommandBufferPool.Get(m_ProfilerTag);
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();




            // 设置源和目标RT为本次渲染的RT 在Execute里进行 特殊处理后处理后注入点
            mDesRT = renderingData.cameraData.renderer.cameraColorTargetHandle;
            mSourceRT = renderingData.cameraData.renderer.cameraColorTargetHandle;





            // 根据激活后处理数量执行不同的渲染策略

            if (mShowcaseProcessingIndex.Count == 1)
            {
                // 激活 一个后处理
                // 如果只有一个激活的后处理，直接在 mTempRT0 上渲染。
                int index = mShowcaseProcessingIndex[0];
                using (new ProfilingScope(cmd, m_ProfilingSamplers[index]))
                {
                    mShowcaseProcessings[index].Render(cmd, ref renderingData, mSourceRT, mTempRT0);
                }
            }
            else
            {
                // 激活多个后处理
                // 如果有多个激活的后处理，依次在 mTempRT0 和 mTempRT1 之间交换并渲染。

                Blitter.BlitCameraTexture(cmd, mSourceRT, mTempRT0);
                for (int i = 0; i < mShowcaseProcessingIndex.Count; i++)
                {
                    int index = mShowcaseProcessingIndex[i];
                    var customProcessing = mShowcaseProcessings[index];

                    using (new ProfilingScope(cmd, m_ProfilingSamplers[index]))
                    {
                        customProcessing.Render(cmd, ref renderingData, mTempRT0, mTempRT1);
                    }


                    CoreUtils.Swap(ref mTempRT0, ref mTempRT1);
                }
            }
            Blitter.BlitCameraTexture(cmd, mTempRT0, mDesRT);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);

        }

        // 释放临时纹理
        public void Dispose()
        {
            RTHandles.Release(mTempRT0);
            RTHandles.Release(mTempRT1);
        }

    }
}

```





### Volume eg.
```plain
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Showcase.ShowcaseURP
{
    // 自定义组件
    [VolumeComponentMenu("Showcase-PostProcessing/Color Tint")]

    public class ColorTint : ShowcaseProcessing
    {

        // 创建材质制定Shader路径
        private Material mMaterial;
        private const string mShaderName = "Showcase/ColorTint";

        // 设置颜色参数
        public BoolParameter useTint = new BoolParameter(false);
        public BoolParameter useAddOn = new BoolParameter(false);
        public ColorParameter ColorChange = new ColorParameter(Color.white, true);
        public ColorParameter ColorAddOn = new ColorParameter(Color.white, true);

        //private bool bUseTint = useTint.value; 

        // 是否应用后处理
        public override bool IsActive() => mMaterial != null && (IsColorFilterActive());
        // 判断设置颜色
        private bool IsColorFilterActive() => useTint == true || useAddOn == true;

        // 设置渲染流程中的注入点
        public override BasicInjectionPoint InjectionPoint => BasicInjectionPoint.AfterPostProcess;
        public override int OrderInInjectionPoint => 15;


        // 配置当前后处理 创建对应的材质
        public override void Setup()
        {
            if (mMaterial == null)
                mMaterial = CoreUtils.CreateEngineMaterial(mShaderName);
        }

        // 执行渲染逻辑
        public override void Render(CommandBuffer cmd, ref RenderingData renderingData, RTHandle source, RTHandle destination)
        {
            if (mMaterial == null) return;
            if (useTint == true)
            {
                mMaterial.SetColor("_ColorTint", ColorChange.value);
            }
            else
            {
                //Debug.Log(ColorChange.overrideState);
                ColorChange.overrideState = false;
                //Debug.Log(ColorChange.overrideState);
                //mMaterial.SetColor("_ColorTint", ColorChange.value);
            }

            mMaterial.SetColor("_ColorAdjust", ColorAddOn.value);
            cmd.Blit(source, destination, mMaterial, 0);
        }

        
        // 清理临时RT
        public override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            CoreUtils.Destroy(mMaterial);
        }
    }
}

```

继承抽象基类后，可在Volume内编写渲染逻辑，由RenderFeature自动获取并自动创建RenderPass

