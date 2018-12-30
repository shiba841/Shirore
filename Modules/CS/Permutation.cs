using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;

public class PermTest {
    public static void Main() {
        int[] a = {1, 2, 3, 4, 5};
        var perms = Permutation<int>.Permutate(a, 4, true);
        WriteLine("num of perms: {0}", perms.Count());
        foreach (var i in perms) {
            WriteLine(String.Join(", ", i));
        }
    }
}

// 順列を求める用モジュール
static class Permutation<T> {
    // 選択数
    private static int selectNum;
    // 重複を許すか否か
    private static bool duplicate;
    
    // メインの再帰メソッド
    private static void Select(List<T[]> perms, Stack<T> stack, List<T> list) {
        // 選択数分スタックされたらリストに順列を追加
        if (stack.Count == selectNum) {
            perms.Add(stack.Reverse().ToArray());
        }
        // 順にスタック
        else {
            var subList = new List<T>(list);
            // 重複を許さない場合使用済み要素は削除
            if (!duplicate) {
                subList.RemoveAt(0);
            }
            for (var i = 0; i < list.Count(); i++) {
                stack.Push(list[i]);
                Select(perms, stack, subList);
                // 選択されていない要素をサブリストに回す
                if (i < subList.Count()) {
                    subList[i] = list[i];
                }
                stack.Pop();
            }
        }
    }
    
    public static IEnumerable<T[]> Permutate(IEnumerable<T> src, int n = 0, bool dupl = false) {
        var perms = new List<T[]>();
        var srcList = new List<T>(src);
        
        // 選択数 <= 要素数のチェック
        if (n <= srcList.Count()) {
            selectNum = n > 0 ? n : srcList.Count();
            duplicate = dupl;
            Select(perms, new Stack<T>(), srcList);
            return perms;
        }
        else {
            WriteLine("num of select is too large");
            return perms;
        }
    }
}