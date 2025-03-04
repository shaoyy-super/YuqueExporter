<font style="color:rgb(51, 51, 51);">Stencil</font>

<font style="color:rgb(51, 51, 51);">{</font>

<font style="color:rgb(51, 51, 51);">    Ref [_Stencil]</font>

<font style="color:rgb(51, 51, 51);">    Comp [_StencilComp]</font>

<font style="color:rgb(51, 51, 51);">    Pass [_StencilOp]</font>

<font style="color:rgb(51, 51, 51);">    ReadMask [_StencilReadMask]</font>

<font style="color:rgb(51, 51, 51);">    WriteMask [_StencilWriteMask]</font>

<font style="color:rgb(51, 51, 51);">}</font>



**<font style="color:rgb(51, 51, 51);"></font>**

**<font style="color:rgb(51, 51, 51);">ReadMask和WriteMask含义</font>**

**<font style="color:rgb(51, 51, 51);"></font>**

**<font style="color:rgb(51, 51, 51);">ReadMask</font>**

<font style="color:rgb(51, 51, 51);">ReadMask  readMask</font>

<font style="color:rgb(51, 51, 51);">ReadMask 从字面意思的理解就是读遮罩，readMask将和referenceValue以及stencilBufferValue进行按位与（&）操作，readMask取值范围也是0-255的整数，默认值为255，二进制位11111111，即读取的时候不对referenceValue和stencilBufferValue产生效果，读取的还是原始值。</font>

**<font style="color:rgb(51, 51, 51);">WriteMask</font>**

<font style="color:rgb(51, 51, 51);">WriteMask writeMask</font>

<font style="color:rgb(51, 51, 51);">WriteMask是当写入模板缓冲时进行掩码操作（按位与【&】），writeMask取值范围是0-255的整数，默认值也是255，即当修改stencilBufferValue值时，写入的仍然是原始值。</font>

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);">简单解释就是</font>

**<font style="color:rgb(51, 51, 51);">ReadMask</font>**

<font style="color:rgb(51, 51, 51);">我只读我想读的位，不管他原来模板值是多少</font>

<font style="color:rgb(51, 51, 51);">举例1</font>

<font style="color:rgb(51, 51, 51);">Stencil</font>

<font style="color:rgb(51, 51, 51);">{</font>

<font style="color:rgb(51, 51, 51);">    Ref 16</font>

<font style="color:rgb(51, 51, 51);">    ReadMask 16</font>

<font style="color:rgb(51, 51, 51);">    Comp </font><u><font style="color:rgb(51, 51, 51);">Equal</font></u>

<font style="color:rgb(51, 51, 51);">}</font>

<font style="color:rgb(51, 51, 51);">Ref 16 当前shader是16，即00001000</font>

<font style="color:rgb(51, 51, 51);">ReadMask 16 只读模板缓冲区里的xxxx1xxx，只要第四位是1则模板通过</font>

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);">举例2</font>

<font style="color:rgb(51, 51, 51);">Stencil</font>

<font style="color:rgb(51, 51, 51);">{</font>

<font style="color:rgb(51, 51, 51);">    Ref 48</font>

<font style="color:rgb(51, 51, 51);">    ReadMask 48</font>

<font style="color:rgb(51, 51, 51);">    Comp </font><u><font style="color:rgb(51, 51, 51);">NotEqual</font></u>

<font style="color:rgb(51, 51, 51);">}</font>

<font style="color:rgb(51, 51, 51);">Ref 48 当前shader是48，即00011000</font>

<font style="color:rgb(51, 51, 51);">ReadMask 16 只读模板缓冲区里的xxx11xxx，只要第四位和第五位同时是1则模板不通过</font>

**<font style="color:rgb(51, 51, 51);">WriteMask同理</font>**

**<font style="color:rgb(51, 51, 51);"></font>**

**<font style="color:rgb(51, 51, 51);"></font>**

**<font style="color:rgb(51, 51, 51);">为了使得各个后处理效果互不干扰，就要使各个后处理使用的Mask（即模板位数）有各自的的位数</font>**

**<font style="color:rgb(51, 51, 51);">目前CustomTAA使用的是16，则模板值第五位</font>**

**<font style="color:rgb(51, 51, 51);">SSAO使用的是32，则模板值第六位</font>**

**<font style="color:rgb(51, 51, 51);">景深使用的是64，则模板值第七位</font>**

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);"></font>

<font style="color:rgb(51, 51, 51);"></font>

