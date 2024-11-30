using System;
using System.IO;

namespace LuminLand.Skinpatch.Installer
{
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
                    Console.WriteLine();
                    
                    string game_modsDir = Path.Combine(gameDir, "mods");
                    string modFilePath = Path.Combine(sourceDir, "mods", "CustomSkinLoader_Fabric-14.21.2.jar");

                    // 如果游戏目录 mods 文件夹不存在，提示错误并退出
                    if (!Directory.Exists(game_modsDir))
                    {
                        Console.WriteLine("游戏目录 mods 文件夹不存在!");
                        Console.WriteLine("请确保游戏版本及 Mod Loader 版本正确！");
                        Exit();
                        return;
                    }
                    
                    Console.WriteLine("开始复制文件...");

                    // 如果游戏目录 mods 文件夹存在 CustomSkinLoader MOD，跳过复制
                    string existingModPath = Path.Combine(game_modsDir, "CustomSkinLoader_Fabric-14.21.2.jar");
                    if (File.Exists(Path.Combine(game_modsDir, "CustomSkinLoader_Fabric-14.21.2.jar")))
                    {
                        Console.WriteLine("MOD-CustomSkinLoader已存在，跳过复制...");
                    }
                    // 否则复制 CustomSkinLoader_Fabric-14.21.2.jar 到游戏目录 mods 文件夹
                    else
                    {
                        Console.WriteLine("正在复制 MOD-CustomSkinLoader...");
                        File.Copy(modFilePath, existingModPath, true);
                        Console.WriteLine("完成！");
                    }

                    Console.WriteLine();

                    // 复制补丁配置文件
                    string game_ModConfigDir = Path.Combine(gameDir, "CustomSkinLoader");
                    string configFilePath = Path.Combine(sourceDir, "CustomSkinLoader", "CustomSkinLoader.json");

                    // 如果存在，直接复制 CustomSkinLoader.json 文件
                    if (Directory.Exists(game_ModConfigDir))
                    {
                        Console.WriteLine("正在复制配置文件 CustomSkinLoader.json...");
                        File.Copy(configFilePath, Path.Combine(game_ModConfigDir, "CustomSkinLoader.json"), true);
                        Console.WriteLine("完成！");
                    }
                    // 如果不存在，先创建 CustomSkinLoader 文件夹，再复制 CustomSkinLoader.json 文件
                    else
                    {
                        Console.WriteLine("创建 CustomSkinLoader 文件夹并复制配置文件 CustomSkinLoader.json...");
                        Directory.CreateDirectory(game_ModConfigDir);
                        File.Copy(configFilePath, Path.Combine(game_ModConfigDir, "CustomSkinLoader.json"), true);
                        Console.WriteLine("完成！");
                    }

                    Console.WriteLine("补丁安装完成！");
                    Exit();
                }
                else
                {
                    Console.WriteLine("无效路径！");
                    Exit();
                }
            }
            else
            {
                Console.WriteLine("未输入路径！");
                Exit();
            }
        }

        private static void Exit()
        {
            Console.WriteLine();
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }
    }
}