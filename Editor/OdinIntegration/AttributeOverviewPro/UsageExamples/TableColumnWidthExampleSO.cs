using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RunLab.AesirInspector.OdinIntegration.Editor
{
    [AesirExample]
    public class TableColumnWidthExampleSO : AttributeExampleSO<TableColumnWidthExampleSO>
    {
        [Title("No Parameters")]
        [TableList]
        public List<Row> defaultWidthItems = new List<Row>
        {
            new Row { ID = 1, Name = "Apple" },
            new Row { ID = 2, Name = "Banana" }
        };

        [Title("Parameter: width")]
        [TableList]
        public List<FixedRow> fixedWidthItems = new List<FixedRow>
        {
            new FixedRow { ID = 1, Name = "Apple" },
            new FixedRow { ID = 2, Name = "Banana" }
        };

        [Title("Parameter: resizable = false")]
        [TableList]
        public List<NonResizableRow> nonResizableItems = new List<NonResizableRow>
        {
            new NonResizableRow { ID = 1, Name = "Apple" },
            new NonResizableRow { ID = 2, Name = "Banana" }
        };

        public override void AesirInspectorReset()
        {
            defaultWidthItems = new List<Row>
            {
                new Row { ID = 1, Name = "Apple" },
                new Row { ID = 2, Name = "Banana" }
            };
            fixedWidthItems = new List<FixedRow>
            {
                new FixedRow { ID = 1, Name = "Apple" },
                new FixedRow { ID = 2, Name = "Banana" }
            };
            nonResizableItems = new List<NonResizableRow>
            {
                new NonResizableRow { ID = 1, Name = "Apple" },
                new NonResizableRow { ID = 2, Name = "Banana" }
            };
        }

        [Serializable]
        public class Row
        {
            public int ID;
            public string Name;
        }

        [Serializable]
        public class FixedRow
        {
            [TableColumnWidth(50)]
            public int ID;

            public string Name;
        }

        [Serializable]
        public class NonResizableRow
        {
            [TableColumnWidth(80, false)]
            public int ID;

            public string Name;
        }
    }
}
