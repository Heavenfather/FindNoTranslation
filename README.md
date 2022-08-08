# FindNoTranslation
 * 查找未翻译文本控制台工具
 * 目前可以根据配置支持读取excel文件、txt文件、csv文件，从中抽取出需要查找的语言
 * 如需要拓展读取不同的类型文件，可以继承自ReadFileBase类写具体的读取逻辑
  >父类的`IsSkip()`，可以选择重写该虚函数，该作用是遇到指定的文本时会选择跳过不会添加到查找中
 * 控制台运行时需要读取AppSetting.txt文件，该文件需要放在运行程序集的同目录下,配置如:
  <img src="https://github.com/Heavenfather/FindNoTranslation/Images/AppSetting.png">
 * 暂时不提供直接用的包体，先直接clone代码运行跑一跑