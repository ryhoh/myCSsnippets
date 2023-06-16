using System;
using System.Collections;
using System.IO;


namespace TableLib
{
    public class Table
    {
        public string[] header;
        public ArrayList data;

        // Constructor
        public Table(string[] header, ArrayList data)
        {
            this.header = header;
            this.data = data;
        }
        
        // ReadTable
        public static Table ReadCsv(string path, bool with_header=true)
        {
            ArrayList data = ReadTableData(path, ',');
            string[] header = null;
            if (with_header)
            {
                header = (string[]) data[0];
                data.RemoveAt(0);
            }
            return new Table(header, data);
        }

        // Print on console
        public void Print()
        {
            Console.WriteLine(this.ToString());
        }

        // Write to file
        public void WriteCsv(string path)
        {
            Write(path, ",");
        }

        // Get column by name
        public int GetColByName(string colName)
        {
            if (header == null)
            {
                throw new Exception("Table has no header");
            }
            else
            {
                return Array.IndexOf(header, colName);
            }
        }

        // Sort by column
        public void SortByCol(int colIndex)
        {
            data.Sort(new ColComparer(colIndex));
        }

        // ColComparer
        private class ColComparer : IComparer
        {
            private int colIndex;

            public ColComparer(int colIndex)
            {
                this.colIndex = colIndex;
            }

            public int Compare(object x, object y)
            {
                string[] row1 = (string[]) x;
                string[] row2 = (string[]) y;
                return row1[colIndex].CompareTo(row2[colIndex]);
            }
        }

        // Represent Table as string
        public override string ToString()
        {
            string output = "";
            // write header
            if (header != null)
            {
                foreach (string col in header)
                {
                    output += col + " ";
                }
                output += "\n";

                // write line
                foreach (string col in header)
                {
                    output += "----";
                }
            }
            output += "\n";

            // write data
            foreach (string[] row in data)
            {
                foreach (string col in row)
                {
                    output += col + " ";
                }
                output += "\n";
            }
            return output;
        }

        private void Write(string path, string delimiter=",")
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                // write header
                if (header != null)
                {
                    file.Write(String.Join(delimiter, header));
                    file.Write("\n");
                }

                // write data
                foreach (string[] row in data)
                {
                    file.Write(String.Join(delimiter, row));
                    file.Write("\n");
                }
            }
        }

        // GetColIndexByExcelStyle
        // Excel style
        //   A = 1
        //   B = 2
        //   ...
        //   Z = 26
        //   AA = 27
        public static int GetColIndexByExcelStyle(string col)
        {
            int index = 0;
            int pow = 1;
            for (int i = col.Length - 1; i >= 0; i--)
            {
                index += (col[i] - 'A' + 1) * pow;
                pow *= 26;
            }
            return index;
        }
        
        // ReadTableData
        private static ArrayList ReadTableData(string path, char delimiter=',')
        {
            ArrayList data = new ArrayList();
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(delimiter);
                    data.Add(values);
                }
            }
            return data;
        }
    }
}
