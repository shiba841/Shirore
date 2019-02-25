using System;
using System.Linq;

public class ComicAlignment {
    
    public static void Main () {
        var array = {2, 5, 3, 1};
        var N = array.Length;
        
        var quickSort = new QuickSort();
        var sortedArray = quickSort.Sort(array, 0, N-1);
    }
    
}

// クイックソート用クラス
public class QuickSort {
    
    // ソート
    public int[] Sort<T> (T[] array, int left, int right) where T : IComparable<T> {
        if (left < right) {
            var pivot = Median(array[left], array[(left + right) / 2], array[right]);
            var l = left;
            var r = right;
            
            while (true) {
                while (array[l].CompareTo(pivot) < 0) {
                    l++;
                }
                while (pivot.CompareTo(array[r]) < 0) {
                    r--;
                }
                if (r <= l) {
                    break;
                }
                
                Swap(ref array[l], ref array[r]);
                l++;
                r--;
            }
            
            Sort(array, left, l-1);
            Sort(array, r+1, right);
        }
        
        return array;
    }
    
    // 中央値計算
    private T Median<T> (T a, T b, T c) where T : IComparable<T> {
        if (a.CompareTo(b) < 0) {
            return a.CompareTo(c) < 0 ? (b.CompareTo(c) < 0 ? b : c) : a;
        }
        else {
            return a.CompareTo(c) > 0 ? (b.CompareTo(c) > 0 ? b : c) : a;
        }
    }
    
    // 要素の入れ替え
    private void Swap<T> (ref T a, ref T b) {
        var tmp = a;
        a = b;
        b = tmp;
    }
}