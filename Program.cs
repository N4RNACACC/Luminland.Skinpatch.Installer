using System;
using System.IO;

namespace LuminLand.Skinpatch.Installer;

class Program
{
    private static void Main()
    {
        Console.Title = "Luminland.Skinpatch.Installer";
        Console.WriteLine("适用于Fabric端Minecraft-1.21.3游戏的游玩Luminland服务器时遇到皮肤问题的补丁自动安装器");
        Console.WriteLine("Github: https://github.com/N4RNACACC/Luminland.Skinpatch.Installer");
        Console.WriteLine();
            
        // 当前目录
        var sourceDir = Directory.GetCurrentDirectory();
        // 输入游戏目录
        Console.WriteLine("请输入游戏的完整路径(*/.minecraft/versions/(需要安装补丁的版本)/):");
        var gameDir = Console.ReadLine();
        Console.WriteLine();

        // 判断输入是否为空
        if (!string.IsNullOrEmpty(gameDir))
        {
            // 判断游戏路径是否正确
            if (gameDir.Contains(".minecraft"))
            {
                Console.WriteLine("目标路径:" + gameDir);
                Console.WriteLine();
                    
                var game_modsDir = Path.Combine(gameDir, "mods"); // 目标游戏mods文件夹路径
                var modFilePath = Path.Combine(sourceDir, "mods", "CustomSkinLoader_Fabric-14.21.2.jar"); // 补丁mod文件路径

                // 如果目标游戏目录 mods 文件夹不存在，提示错误并退出
                if (!Directory.Exists(game_modsDir))
                {
                    Console.WriteLine("游戏目录 mods 文件夹不存在!");
                    Console.WriteLine("请确保游戏版本及 Mod Loader 版本正确！");
                    Exit();
                    return;
                }
                    
                Console.WriteLine("开始复制文件...");
                
                // 如果游戏目录 mods 文件夹存在 CustomSkinLoader MOD，跳过复制
                var existingModPath = Path.Combine(game_modsDir, "CustomSkinLoader_Fabric-14.21.2.jar");
                // 判断 CustomSkinLoader MOD 是否存在，若存在返回 true
                var modExists = Directory.GetFiles(game_modsDir, "CustomSkinLoader*.jar").Length > 0;
                if (modExists)
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
                var game_ModConfigDir = Path.Combine(gameDir, "CustomSkinLoader");
                var patch_configFilePath = Path.Combine(sourceDir, "CustomSkinLoader", "CustomSkinLoader.json");

                // 如果存在，直接复制 CustomSkinLoader.json 文件
                if (Directory.Exists(game_ModConfigDir))
                {
                    Console.WriteLine("正在复制配置文件 CustomSkinLoader.json...");
                    File.Copy(patch_configFilePath, Path.Combine(game_ModConfigDir, "CustomSkinLoader.json"), true);
                    Console.WriteLine("完成！");
                }
                // 如果不存在，先创建 CustomSkinLoader 文件夹，再复制 CustomSkinLoader.json 文件
                else
                {
                    Console.WriteLine("创建 CustomSkinLoader 文件夹并复制配置文件 CustomSkinLoader.json...");
                    Directory.CreateDirectory(game_ModConfigDir);
                    File.Copy(patch_configFilePath, Path.Combine(game_ModConfigDir, "CustomSkinLoader.json"), true);
                    Console.WriteLine("完成！");
                }

                Console.WriteLine("补丁安装完成！");
                Console.WriteLine("启动游戏并进入任一服务器或者本地游戏即可生效。");
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