using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;

public class CombTest {
    public static void Main() {
        int[] a = {1, 2, 3, 4};
        var combs = Combination<int>.Combine(a, 3, true);
        WriteLine("num of combs: {0}", combs.Count());
        foreach (var i in combs) {
            WriteLine(String.Join(", ", i));
        }
    }
}

// 組み合わせを求める用モジュール
static class Combination<T> {
    // 選択数
    private static int selectNum;
    // 重複を許すか否か
    private static bool duplicate;
    
    // メインの再帰メソッド
    private static void Select(List<T[]> combs, Stack<T> stack, List<T> list) {
        // 選択数分スタックされたらリストに組み合わせを追加
        if (stack.Count == selectNum) {
            combs.Add(stack.Reverse().ToArray());
        }
        // 順にスタック
        else if (list.Count() > 0){
            var subList = new List<T>(list);
            // 重複を許さない場合使用済み要素は削除
            if (!duplicate) {
                subList.RemoveAt(0);
            }
            for (var i = 0; i < list.Count(); i++) {
                stack.Push(list[i]);
                Select(combs, stack, subList);
                // 組み合わせなので使用済み要素は削除
                if (subList.Count() > 0) {
                    subList.RemoveAt(0);
                }
                stack.Pop();
            }
        }
    }
    
    public static IEnumerable<T[]> Combine(IEnumerable<T> src, int n = 0, bool dupl = false) {
        var combs = new List<T[]>();
        var srcList = new List<T>(src);
        
        // 選択数 <= 要素数のチェック
        if (n <= srcList.Count()) {
            selectNum = n > 0 ? n : srcList.Count();
            duplicate = dupl;
            Select(combs, new Stack<T>(), srcList);
            return combs;
        }
        else {
            WriteLine("num of select is too large");
            return combs;
        }
    }
}