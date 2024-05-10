namespace EasyTranslate.Infrastructure.GameData;

using System.Collections.Concurrent;
using System.Reflection;
using Lumina.Data.Files.Excel;
using Lumina.Data.Structs.Excel;
using Lumina.Excel;

// TODO: Remove if / when this functionality gets merged into upstream Lumina
public static class ExcelSheetExtension
{
    public static IEnumerable<T> GetCachelessEnumerator<T>(this ExcelSheet<T> excelSheet) where T : ExcelRow
    {
        ExcelDataFile file = null!;
        RowParser parser = null!;

        foreach (var offset in excelSheet.GetRowDataOffsets())
        {
            var rowPtr = offset.RowOffset;
            if (file != offset.SheetPage)
            {
                parser = new RowParser(excelSheet, offset.SheetPage);
            }

            if (excelSheet.Header.Variant == ExcelVariant.Subrows)
            {
                // required to read the row header out and know how many subrows there is
                parser.SeekToRow(rowPtr.RowId);

                // read subrows
                for (uint i = 0; i < parser.RowCount; i++)
                {

                    var cacheKey = excelSheet.GetCacheKey(rowPtr.RowId, i);
                    if (excelSheet.GetRowCache().TryGetValue(cacheKey, out var cachedRow))
                    {
                        yield return cachedRow;
                        continue;
                    }

                    var obj = excelSheet.ReadSubRowObjExt(parser, rowPtr.RowId, i);
                    yield return obj;
                }
            }
            else
            {
                var cacheKey = excelSheet.GetCacheKey(rowPtr.RowId);
                if (excelSheet.GetRowCache().TryGetValue(cacheKey, out var cachedRow))
                {
                    yield return cachedRow;
                    continue;
                }

                var obj = excelSheet.ReadRowObjExt(parser, rowPtr.RowId);
                yield return obj;
            }
        }
    }

    private static ulong GetCacheKey<T>(this ExcelSheet<T> excelSheet, uint rowId, uint subrowId = uint.MaxValue)
        where T : ExcelRow
    {
        return (ulong)excelSheet.GetType().BaseType!.GetMethod(
            "GetCacheKey",
            BindingFlags.Static | BindingFlags.NonPublic)!.Invoke(excelSheet, [rowId, subrowId])!;
    }

    private static ConcurrentDictionary<UInt64, T> GetRowCache<T>(this ExcelSheet<T> excelSheet) where T : ExcelRow
    {
        return (ConcurrentDictionary<ulong, T>)excelSheet.GetType().GetField(
            "_rowCache",
            BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(excelSheet)!;
    }

    private static T ReadSubRowObjExt<T>(this ExcelSheet<T> excehSheet, RowParser parser, uint rowId, uint subRowId)
        where T : ExcelRow
    {
        return (T)excehSheet.GetType().GetMethod("ReadSubRowObj", BindingFlags.Instance | BindingFlags.NonPublic)!
                            .Invoke(excehSheet, [parser, rowId, subRowId])!;
    }

    private static T ReadRowObjExt<T>(this ExcelSheet<T> excelSheet, RowParser parser, uint rowId)
        where T : ExcelRow
    {
        return (T)excelSheet.GetType().GetMethod("ReadRowObj", BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(
            excelSheet,
            [parser, rowId])!;
    }
}
