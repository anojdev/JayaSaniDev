

using LINQ;
using System.Runtime.CompilerServices;

List<Employee> employees = new List<Employee>()
{

    new Employee{Email="anoj@gmail.com",Salary=200,Name="Anoj Shrestha",Group="Green",DepartmentId=1},
     new Employee{Email="akshada@gmail.com",Salary=350,Name="Akshada Shrestha",Group = "Green",DepartmentId = 1},
      new Employee{Email="prakriti@gmail.com",Salary=250,Name="Prakriti Pradhan",Group = "Red",DepartmentId=2},
       new Employee{Email="suraj@gmail.com",Salary=250,Name="Suraj Shrestha", Group = "Red",DepartmentId=2},
         new Employee{Email="pravin@gmail.com",Salary=550,Name="Pravin Pradhan", Group = "Yellow",DepartmentId=3},
          new Employee{Email="raju@gmail.com",Salary=600,Name="Raju Tamang", Group = "Yellow",DepartmentId=4},
           new Employee{Email="Sita@gmail.com",Salary=700,Name="Sita Kangshakar", Group = "Yellow",DepartmentId=2},
            new Employee{Email="Pabi@gmail.com",Salary=450,Name="Pabitra Khanal", Group = "Green",DepartmentId=3},
             new Employee{Email="Ramita@gmail.com",Salary=1100,Name="Ramita Dahal", Group = "Yellow",DepartmentId=2},
             // new Employee{Email="Ramita@gmail.com",Salary=1100,Name="Ramita Dahal", Group = "Yellow",DepartmentId=10}
};
List<Employee> employeeNew = new List<Employee>()
{
    new Employee{Email="gokarna@gmail.com",Salary=150,Name="Gokarna Shrestha",Group="Purple"},
     new Employee{Email="shanta@gmail.com",Salary=350,Name="Shanta Shrestha",Group = "Yellow"},
      new Employee{Email="anoj@gmail.com",Salary=200,Name="Anoj Shrestha",Group="Purple"},
};

List<Department> departments = new List<Department>()
{
    new Department{DepartmentId=1,DepartmentName="IT"},
    new Department{DepartmentId=2,DepartmentName="CIVIL"},
    new Department{DepartmentId=3,DepartmentName="ACCOUNT"},
      new Department{DepartmentId=4,DepartmentName="FARM"},
};

#region Select

//Single Select in Linq
var names = from e in employees select e.Name;

//Multiple Select in Linq

var selectedFields = from e in employees
                     select new {e.Name,e.Salary};

//Single Select in Lambda

var lambdaEmployee = employees.Select(x => x.Email);

//Multiple Select in Lambda
var lambdaMultipleSelect = employees.Select(x=> new {x.Name,x.Salary});

#endregion 

#region Where
var linqWhere = from e in employees where e.Salary > 200 select new {e.Name,e.Salary};

var lambdaWhere = employees.Where(x=>x.Salary > 250).Select(x=> new {x.Name,x.Salary});

#endregion

#region OrderBy / OrderByDescending
var linqOrderBy = from e in employees orderby e.Name ascending,e.Salary descending select(new {e.Name,e.Salary});//asending by default

var lambdaOrderBy = employees.OrderBy(x=>x.Name).ThenByDescending(x=>x.Salary);
#endregion

#region Aggregations
//1.Count
var empCountLinq = (from e in employees select e).Count();
var empCountLambda = employees.Count();

//2.Sum
var empSumLinq = (from e in employees select e.Salary).Sum();
var empSumLambda = employees.Sum(x=>x.Salary);

//3.Average 
var avgSalaryLinq = (from e in employees select e.Salary).Average();
var avgSalaryLambda = employees.Average(x => x.Salary);

//4. Min/Max
var minSalaryLinq = (from e in employees select e.Salary).Min();//Max()
var maxSalaryLambda = employees.Max(x=>x.Salary);

#endregion

#region Set Operation
//1. Distinct
var uniqueDepartmentsLinq = (from e in employees select e.Group).Distinct();
var uniqueDepartmentLambda = employees.Select(x=>x.Group).Distinct();

//2. Union
var unionLinq = (from item in employees select item).Union(employeeNew);
var unionLambda = employees.Union(employeeNew);

//3. Intersect and Except accordingly


#endregion

#region Join

#region Inner Join
//Inner Join
var innerJoinLinq = from e in employees
                    join d in departments
                    on e.DepartmentId equals d.DepartmentId
                    select new { e.Name, e.Salary, d.DepartmentName };

var innerJoinLambda = departments.GroupJoin(employees,
    d => d.DepartmentId,
    e => e.DepartmentId,
    (d, empGroup) => new
    {
        d.DepartmentName,
        d.DepartmentId,
        employees = empGroup.Select(x => new
        {
            x.Name,
            x.Salary
        })
    });
#endregion

#region Left Outer Join
var left = from e in employees
           join d in departments on e.DepartmentId equals d.DepartmentId into deptGroup
           from department in deptGroup.DefaultIfEmpty()
           select new
           {
               e.EmployeeId,
               e.Email,
               DepartmentName = department?.DepartmentName ?? "No Department"
           };
#endregion

#endregion

#region Grouping

var groupbyLinq = from e in employees
                  group e by e.DepartmentId into deptGroup 
                  select deptGroup;

var groupByLambda = employees.GroupBy(e => e.DepartmentId);

#endregion

#region Projection
//Flattening Nested Collection (SelectMany) ???
#endregion

#region Pagination
var pagedEmployeesLinq = (from e in employees select e).Skip(2).Take(3);
var pagedEmployeesLambda = employees.Skip(2).Take(3);
#endregion

#region Deferred vs Immediate Execution

#endregion

#region Advance Scenario
//1. First
//var firstEmployeeLinq = (from e in employees where e.Salary>4000 select e).First(); //throws an exception if null
//var firstEmployeeLambda = employees.First(x => x.Salary > 5000);//throws an exception if null

//2. FirstOrDefault
var firstOrDefaultLLinq = (from e in employees where e.Salary > 5000 select e).FirstOrDefault();//No Exception even if null
var firstOrDefaultLambda = employees.FirstOrDefault(x => x.Salary > 4000);//No Exception even if null

//3. Single -> only give result if result has single value
var singleEmployeeLinq = (from e in employees where e.Salary > 1000 select e).Single();//Error if null
var singleEmployeeLambda = employees.Single(x => x.Salary > 1000);
//Console.WriteLine(singleEmployeeLinq.Name);

//4. SingleOrDefault -> 
var singleOrDefaultLinq = (from e in employees where e.Name.Contains("Anoj") select e).SingleOrDefault();
var singleOrDefaultLambda = employees.SingleOrDefault(x => x.Salary > 1000);

#endregion


foreach (var item in left)
{
    //Console.WriteLine(dep.Key);
    //foreach(var emp in dep)
    //{
    //    Console.WriteLine(emp.Name + "->"+emp.Email);
    //}

    //Console.WriteLine($"Employee : {item.Email}, Department: {item.DepartmentName}");
}

#region String LINQ

#region String.Distinct

string s = "aabbcc";
string unique = new string(s.Distinct().ToArray());
//Console.WriteLine(unique);
#endregion

#region String.Count
string s1 = "Hello";
int count = s1.Count(c => c == 'l');
//Console.WriteLine(count);
#endregion

#region String.StartWith , String.EndWith
 string s2 = "Hello Nepal";
bool result = s2.StartsWith("Hello");
//Console.WriteLine(result);
#endregion

#region String.Concat
string ss = "Anoj";
string ss1 = "Kumar";
string resultSS = string.Concat(ss,string.Empty, ss1);
//Console.WriteLine(resultSS);
#endregion

#region string.PadLeft, string.PadRight

string sPadLeft = "Anoj";
string padLeft = sPadLeft.PadLeft(5, '*');
//Console.WriteLine(padLeft);

#endregion

#region String.Remove
string sRemove = "Anoj Kumar";
string remove = sRemove.Remove(3);
//Console.WriteLine(remove);
#endregion

#region String.Insert
string sInsert = "HelloAnoj";
string insert = sInsert.Insert(5, "->");
//Console.WriteLine(insert);


#endregion

#region String.Reverse
string sReverse = "Hello";
string reverse = new string(sReverse.Reverse().ToArray());
//Console.WriteLine(reverse);

#endregion

// Comparator----

#region string.ToCharArray
string sArray = "Anoj";
char[] chars = sArray.ToCharArray();
#endregion

#region String.StartWith, String.StartWith with StringComparision

string stringStartWith = "Hello Akshada!";
bool stringEndWith = stringStartWith.StartsWith("hello",StringComparison.OrdinalIgnoreCase);
//Console.WriteLine(stringEndWith);

#endregion

#region String.Intersect
string in1 = "apple";
string in2 = "purple";
string intersect = new string(in1.Intersect(in2).ToArray());
//Console.WriteLine(intersect);

#endregion

//Same concept Union

#region String.LastIndexOf

//"Hello Anoj Hello Kumar Hello Shrestha" ;// Remove Last Hello
string sLastIndexOf = "Hello Anoj Hello Kumar Hello Shrestha";
int lastIndex = sLastIndexOf.LastIndexOf("Hello");

string rem = sLastIndexOf.Remove(lastIndex, 5);
//Console.WriteLine(rem);

#endregion

#region String.OrderBy , String.OrderByDescending

string orderBy = "abcacdeda";
string resultOrderBy = new string(s.OrderBy(c=>c).ToArray());

string desc = new string(orderBy.OrderByDescending(c=>c).ToArray());
//Console.WriteLine(desc);
//Console.WriteLine(resultOrderBy);


#endregion

#region String.Zip
string z1 = "abc";
string z2 = "123";

var zipped = string.Concat(z1.Zip(z2, (c1, c2) => $"{c1}{c2}"));
//Console.WriteLine(zipped);

#endregion

#region String.Count(c=>condition)

string sc = "ab12cd34";
int digitCount = sc.Count(char.IsDigit);
//Console.WriteLine(digitCount);

#endregion

#region String.GroupBy

string sGroupBy = "anoj apple";
var grp = sGroupBy.GroupBy(c => c).Select(x => $"{x.Key}:{x.Count()}");
foreach (var group in grp)
{
    //Console.WriteLine(group);
}

#endregion



#endregion

#region Revise LINQ

 //Inner Join
 var innerJoinRevise = from e in employees
                       join d in departments on e.DepartmentId equals d.DepartmentId
                       select new { e.Name, d.DepartmentName };

var leftOuterJoinRevise = from e in employees
                          join d in departments on e.DepartmentId equals d.DepartmentId into ed
                          from subDep in ed.DefaultIfEmpty()
                          select new { e.Name, Department = subDep?.DepartmentName ?? "No Department" };

var xx = from e in employees
         join d in departments on e.DepartmentId equals d.DepartmentId into ed
         from subDep in ed.DefaultIfEmpty()
         select new { e.Name, DepartmentAnoj = subDep?.DepartmentName ?? "No Department" };



foreach (var group in leftOuterJoinRevise)
{
   // Console.WriteLine(group.Name + "-"+ group.Department);
}

#endregion

#region Extra

// Takewhile
var takeWhile = employees.TakeWhile(x => x.Salary > 199 && x.Salary<500 ).ToList();
foreach (var employee in takeWhile)
{
   // Console.WriteLine(employee.Name +"->"+ employee.Salary);
}

//SkipWhile

var skipWhile = employees.SkipWhile(x => x.Salary < 500).ToList();
foreach (var employee in skipWhile)
{
   // Console.WriteLine(employee.Name + "->"+employee.Salary);
}

//zip
var zip = employees.Zip(employeeNew,(x,y)=>$"{x.Name} => {y.Name}");
Console.WriteLine(zip);
foreach (var employee in zip)
{
    //Console.WriteLine(employee);
}

//Range
//Repeat
var range = Enumerable.Range(5, 5);
foreach (var item in range)
{
   // Console.WriteLine(item);
}

var repeat = Enumerable.Repeat("Anoj", 5);
foreach (var item in repeat)
{
    //Console.WriteLine(item);
}

//Aggregate
var numAggreate = new int[] {2,3,4,5,6,7};
var agg = numAggreate.Aggregate((x,y) => x+y);

//Console.WriteLine(agg);

//Quantifier Operations

//A. All

var allDemo = employees.All(x => x.Salary < 250);
//Console.WriteLine(allDemo);

//Any
var anyDemo = employees.Any(x => x.Salary > 350);
Console.WriteLine(anyDemo);

#endregion