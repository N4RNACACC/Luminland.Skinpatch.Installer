using System;
using System.IO;

namespace LuminLand.Skinpatch.Installer;

class Program
{
    static void Main()
    {
        // 当前目录
        string sourceDir = Directory.GetCurrentDirectory();
        // 输入游戏目录
        Console.WriteLine("请输入游戏的完整路径:");
        string gameDir = Console.ReadLine();

        if (!string.IsNullOrEmpty(gameDir))
        {
            if (gameDir.Contains(".minecraft"))
            {
                Console.WriteLine("目标路径:" + gameDir);

                Console.WriteLine("开始复制文件...");

                // 检查mods文件夹
                string modsDir = Path.Combine(gameDir, "mods");
                string modFilePath = Path.Combine(sourceDir, "mods", "CustomSkinLoader_Fabric-14.21.2.jar");

                // 如果游戏目录mods文件夹存在CustomSkinLoader_Fabric-14.21.2.jar，跳过复制
                if (File.Exists(Path.Combine(modsDir, "CustomSkinLoader_Fabric-14.21.2.jar")))
                {
                    Console.WriteLine("MOD-CustomSkinLoader已存在，跳过复制...");
                }
                // 否则复制CustomSkinLoader_Fabric-14.21.2.jar到游戏目录mods文件夹
                else
                {
                    Console.WriteLine("正在复制MOD-CustomSkinLoader...");
                    Directory.CreateDirectory(modsDir); // 确保mods目录存在
                    File.Copy(modFilePath, Path.Combine(modsDir, "CustomSkinLoader_Fabric-14.21.2.jar"), true);
                }

                Console.WriteLine();

                // 复制补丁配置文件
                string customSkinLoaderDir = Path.Combine(gameDir, "CustomSkinLoader");
                string configFilePath = Path.Combine(sourceDir, "CustomSkinLoader", "CustomSkinLoader.json");

                // 如果存在，直接复制CustomSkinLoader.json文件
                if (Directory.Exists(customSkinLoaderDir))
                {
                    Console.WriteLine("正在复制配置文件CustomSkinLoader.json...");
                    File.Copy(configFilePath, Path.Combine(customSkinLoaderDir, "CustomSkinLoader.json"), true);
                    Console.WriteLine("完成！");
                }
                // 如果不存在，先创建CustomSkinLoader文件夹，再复制CustomSkinLoader.json文件
                else
                {
                    Console.WriteLine("创建CustomSkinLoader文件夹并复制配置文件CustomSkinLoader.json...");
                    Directory.CreateDirectory(customSkinLoaderDir);
                    File.Copy(configFilePath, Path.Combine(customSkinLoaderDir, "CustomSkinLoader.json"), true);
                    Console.WriteLine("完成！");
                }

                Console.WriteLine("补丁安装完成！");
                Console.WriteLine();
                Exit();
            }
            else
            {
                Console.WriteLine("无效路径！");
                Console.WriteLine();
                Exit();
            }
        }
        else
        {
            Console.WriteLine("未输入路径！");
            Console.WriteLine();
            Exit();
        }
    }

    private static void Exit()
    {
        Console.WriteLine("按任意键退出...");
        Console.ReadKey();
    }
}
