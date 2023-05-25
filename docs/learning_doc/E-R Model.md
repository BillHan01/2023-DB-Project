## E-R Model



### 设计过程概览 Overview of the Design Process：

确定用户需求 **==需求分析==**

选择data model **==概念设计==**

实现 

* Logical Design **==逻辑设计==**	Deciding on the database schema

* Physical Design **==物理设计==**   Deciding on the physical layout of the database



**Design Alternatives：**

==avoid==

* Redundancy
* Incompleteness



### 实体关系模型 The Entity-Relational Model

==**Terms：**==

* Relation（关系）：table
* Relationship（联系）：association among several entities



==**ER data model, three basic notions：**==

* ==**entity sets**==

  * entity：an object that exists and is distinguishable from other objects

    * **concrete entities（具体实体）：book，person，company**
    * **abstract entities（抽象实体）：course**

  * <img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230328101458633.png" alt="image-20230328101458633" style="zoom:50%;" />

    <img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230328101530963.png" alt="image-20230328101530963" style="zoom: 67%;" />

    *  **ER Diagram**

      * **Rectangles** represent entity sets

      * **Attributes** listed inside entity rectangle

      * **Underline** indicates **primary key** attributes

        

* ==**relational sets**==

  * A **relationship** is an **association** among several attributes
  * A relationship may also have attributes called **descriptive attributes（描述性属性）**

    * **advisor（导师）关系，是连接instructor（教师）和student（学生）的关系。**在这个关系中，描述性属性可以包括：
      1. **"开始指导日期"(date)：**表示教师开始担任学生导师的时间。
      2. "指导方式"：描述教师与学生沟通和交流的方式，如面对面指导、在线指导等。
      3. "指导频率"：表示教师和学生多久进行一次指导会议，例如每周一次、每月一次等。

  * **Degree：关系涉及的实体集数量**

    * binary（二元）
    * ternary（三元）

  * 

    * <img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230328102417179.png" alt="image-20230328102417179" style="zoom:67%;" />

    <img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230328102517326.png" alt="image-20230328102517326" style="zoom:67%;" />

    ​						<img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230328102723548.png" alt="image-20230328102723548" style="zoom:67%;" />

    ​								<img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230328102832460.png" alt="image-20230328102832460" style="zoom:67%;" />

* ==**attributes**==

  * A entity is represented by a set of attributes
  * **types**
    * **Simple attributes：基本，不可再分，==例如学生的姓名、年龄和性别==**
    * **Composite attributes：由多个相关的简单属性组成，==例如，一个人的地址可以是一个复合属性，它包括街道、城市、省份和邮政编码等简单属性==**
      * <img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230406102918853.png" alt="image-20230406102918853" style="zoom:67%;" />

    * **Single-valued Attributes：是每个实体只具有一个值的属性，==例如，学生的学号、员工的工号等。==**
    * **Multi-valued Attributes：是每个实体可能具有多个值的属性，==例如，一个学生可能有多个电话号码，或者一个员工可能会说多种语言。==**
    * **Stored Attributes：存储属性是==直接存储在数据库中==的属性。==例如，员工的出生日期、姓名等。==**
    * **Derived Attributes：通过其他属性==计算得出==的属性。==例如，员工的年龄可以通过其出生日期和当前日期计算得出。==**
    * **Key Attributes：用于==唯一标识==实体集中的每个实体。例如，学生的学号、员工的工号等。**
    * Domain
      * 每个属性允许的值的集合




### 约束条件 Constrains

**==Mapping Cardinality：（映射基数）==**

* one to one：**==一个人（实体集A）和他的身份证号（实体集B）之间的关系是一对一的==**
* one to many：**==一个老师（实体集A）可以教授多个学生（实体集B）。但每个学生只能由一个老师教授。==**
* many to one：**==多个员工（实体集A）可以属于同一个部门（实体集B）。然而，每个员工只能属于一个部门。==**
* many to many：**==一个学生（实体集A）可以选修多门课程（实体集B），同时，一门课程也可以被多个学生选修。==**
* ==**与Relational（关系）的联系与区别**==
  * 都是描述实体集之间联系的概念
  * 关系：实体集之间的连接
  * 映射基数：实体集之间关系的数量方面
  * ==**关系表示实体之间的关联，而映射基数表示实体关联的多少。**==



==**Keys：（键）**==

* ==**super key**==
  * ==**一个或多个属性的组合**==
  * ==**唯一标识**==实体集中的每个实体
  * **==一个实体集至少有一个超键，但通常会有多个超键。==**
    * **至少有一个属性或属性组合可以唯一标识实体集中的每个实体，因此至少有一个超键。**
    * 然而，在大多数情况下，实体集会有多个超键。这是因为**任何包含一个超键的属性组合也是一个超键。**
  * 在学生实体集中，学号、学号与姓名的组合、学号与班级的组合等都可以作为超键。
* ==**candinate key**==
  * ==**候选键是最小的超键，不能再去掉任何属性的超键。**==
  * **==一个实体集可以有一个或多个候选键==**
  * 例如，在学生实体集中，学号就是一个候选键，因为它可以唯一地标识每个学生。
* ==**primary key**==
  * ==**从实体集的一个或多个候选键中选择的一个特定的候选键**==
  * ==**唯一地标识实体集中的每个实体**==
  * ==**实体集中只能有一个主键**==
  * 主键的选择通常基于实际应用的需要，如易于使用、稳定性等因素。例如，在学生实体集中，学号通常作为主键。
* <img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230417075901686.png" alt="image-20230417075901686" style="zoom: 67%;" />
* <img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230417080232899.png" alt="image-20230417080232899" style="zoom:67%;" />



**==Keys for Relational Sets：（关系集的键）==**

* ==**用于唯一标识关系集中的每个元组**==
* ==**关系集是实体之间关系的集合，每个元组表示一种关系，包括多个实体集中的实体**==
* {student_id, instructor_id} is the ==**super key**== of advisor
* ![image-20230417091431044](C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230417091431044.png)



==**Participation**==

<img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230417091717041.png" alt="image-20230417091717041" style="zoom: 150%;" />



### 实体关系图 Entity-Relationship Diagrams

* 

<img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230417092135823.png" alt="image-20230417092135823" style="zoom:67%;" />

* ![image-20230417092422620](C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230417092422620.png)

  ==**从自己出发的线画在对面**==

  | instructor | student的线                           | advisor | instructor的线                             | student |
  | ---------- | ------------------------------------- | ------- | ------------------------------------------ | ------- |
  |            | 一个student==最多==对应一个instructor |         | 一个instructor可以==对应0个或多个==student |         |

* <img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230417092502702.png" alt="image-20230417092502702" style="zoom:80%;" />

  **==从自己出发的线画在自己这边(看上边的图)==**

  

*==基数约束==关注的是==实体集之间关系的数量级==，通过在ER图中的连接线上标注数字、箭头或其他标记来表示。而==参与==关注的是==实体在关系中的存在程度==，通过在ER图中加粗或使用普通连接线来表示完全参与或部分参与。在表示时，基数约束和参与可以组合使用，以更准确地描述实体之间的关系。==例如，在表示一个学生必须选修一门课程的场景时，可以在连接线上标注“1”表示一对一关系，并加粗连接线表示学生实体集在选课关系中完全参与。==*

*假设我们有一个医院数据库，其中有==医生（Doctors）实体集==和==诊室（Consultation Rooms）实体集==。在这个医院里，==每位医生被分配一个专属的诊室，而每个诊室只能由一个医生使用==。这种情况就构成了==一个一对一关系和完全参与==的结合。*

*假设医院的规定是==每位医生都必须有一个专属诊室，且每个诊室必须分配给一位医生==。在这种情况下，==医生实体集和诊室实体集在分配关系中都是完全参与的。==*





* <img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230417092621283.png" alt="image-20230417092621283" style="zoom:80%;" />

  表示==**Cardinality Limits（基数约束）**==的另一种替代方法

  Instructor can advise 0 or more students. A student must have 1 advisor; cannot have multiple advisors

  **==从自己出发的线画在自己这边==**

  

  

* <img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230417092706367.png" alt="image-20230417092706367" style="zoom:80%;" />

==**Entity sets of a relationship need not be distinct：在一个关系中，一个实体集可以与其他实体集相同。换句话说，这意味着关系可以连接同一个实体集中的实体。这种类型的关系通常称为递归关系（Recursive Relationship）。**==

**==例如，在一个组织结构中，我们有一个实体集“员工（Employees）”，其中每个员工都有一个上级。在这种情况下，我们可以在同一个实体集“员工”内创建一个关系，表示员工与他们的上级之间的关系。这里，实体集“员工”即为源实体集，也是目标实体集。==**

<img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230417163605578.png" alt="image-20230417163605578" style="zoom: 33%;" />

<img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230417163747175.png" alt="image-20230417163747175" style="zoom:33%;" />

* ![image-20230417165148845](C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230417165148845.png)

  ==**判别器用于在弱实体集内区分不同的实体**==

  举一个例子：假设我们有一个“部门（Department）”实体集和一个“员工（Employee）”实体集。

  每个部门都有一个唯一的部门编号（DepartmentID），而员工有一个在部门内唯一的员工编号（EmployeeID）。

  在这个例子中，==**员工实体集是一个弱实体集，因为它依赖于部门实体集来唯一确定每个员工。**==

  ==**员工编号（EmployeeID）是判别器，因为它可以在同一个部门内区分不同的员工。**==

  ==**要唯一地确定一个员工，我们需要结合部门编号（DepartmentID）和员工编号（EmployeeID）。**==

<img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230417165849007.png" alt="image-20230417165849007" style="zoom:150%;" />

* 一个section最多对应一个course
* 每一个section都有一个对应的course
* 一个course可以对应多个section
* sec_course是一个identifying relationship，用双线菱形表示
* section实体集是一个弱实体集，它依赖于课程实体集来唯一确定每个员工
* 判别器用下划虚线表示



* **Summary：**

<img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230330104026214.png" alt="image-20230330104026214" style="zoom:67%;" />

<img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230330104055074.png" alt="image-20230330104055074" style="zoom: 50%;" />





### 扩展E-R特性 Extended E-R Features 



**==高层实体集（Higher-level Entity Set/superclass）==**通常指代一个更为泛化的实体集

* 高层实体集包含一些通用属性和关系，这些属性和关系适用于所有属于它的子实体集。

==**低层实体集（Lower-level Entity Set/Subclass/Derived class）**==是指在分类层次结构中具有更为特定特征的实体集。

* 子类具有超类的所有属性和关系，并可以具有自己特有的属性和关系。
  * 目的是为了==**捕捉现实世界中的分类层次结构**==，以便更好地组织和表示数据。

==**ISA relationship/superclass - subclasss relationship**==

* ISA关系和超类-子类关系这两个概念在ER模型中是==**等价的，并可以互换使用**==。
* <img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230418064303287.png" alt="image-20230418064303287" style="zoom: 80%;" />



==**Specialization（专化）：**==

* 表示将一个实体集（通常称为==**超类或父类**==）根据某些特征或条件==**分解**==为多个子集（称为子类）的过程。
* ==**Top-down design process**==



==**Generalization（泛化）：**==

* 表示将多个具有相似特征的实体集==**合并**==成一个更为泛化的实体集（称为父类或超类）的过程。
* ==**Bottom-up design process**==



==**Aggregation（聚合）：**==

* 表示将一个或多个实体集和它们之间的关系集组合为一个高层次的实体集。
* 允许我们在更高的抽象层次上表示实体集和关系集之间的关系，从而简化数据模型。
* ![image-20230418065210358](C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230418065210358.png)




### 实体关系设计问题 Entity-Relationship Design Issues

==**use the primary key of entity set as an attribute of another entity set,instead of using a relationship**==

*	有两个实体集：“书籍”和“作者”。
  *	书籍实体集具有以下属性：书籍ID（主键）、书名、作者ID；
  *	作者实体集具有以下属性：作者ID（主键）、姓名、国籍。

*	错误地将作者ID（作者实体集的主键）作为书籍实体集的一个属性，而不是使用关系表示书籍与作者之间的联系。

* 这样设计存在以下问题：

  * 数据冗余：如果一个作者写了多本书，那么该作者的信息将在书籍实体集中重复多次。

  * 数据完整性：在书籍实体集中，作者ID可能没有引用完整性约束，这意味着书籍可能关联到一个不存在的作者。

  * 更新和删除操作的复杂性：当需要更新或删除作者信息时，需要在书籍实体集中查找并修改所有与该作者相关的记录

  * 正确的做法是为书籍和作者实体集之间创建一个关系集，如“写作”关系。书籍和作者实体集通过“写作”关系集连接，书籍实体集不再包含作者ID属性。

* ==**虽然看似将作者ID作为书籍实体集的属性，但并未明确表达出书籍与作者之间的关系。**==
* ==**与外键的类比思考**==
  * 外键是一种数据库设计中的==**约束条件**==，用于在实体之间建立关系。
  * ==**是一个实体集中的属性，它引用另一个实体集的主键。**==
  * ==**通过引入外键约束，可以确保引用的实体存在，从而保证数据的引用完整性。**==
  * ==**在上述书籍和作者的例子中，正确的做法应该是将作者ID作为书籍实体集的外键，而不仅仅是一个普通属性。**==
  * ==**在将实体-关系模型转换为关系数据库模式时，实体集和关系集被转换为关系表，实体之间的关系则通过外键来表示。**==



**==designate the primary key attributes of the related entity sets as attributes of the relationship set==**

![image-20230418073251095](C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230418073251095.png)

我们可以==**为“选课”关系创建一个单独的关系表，具有以下属性：**==

- 选课ID（主键）
- 学号（外键，引用“学生”表的主键）
- 课程编号（外键，引用“课程”表的主键）
- 成绩
- 选课日期

通过使用外键约束，我们可以确保数据的引用完整性和一致性。



### 简化到关系模式 Reduction to relation schemas

==**Change relationship sets to relation schemas（将关系集更改为关系模式）**==

==**多对多新建一个表，一对多不用新建一个表，many对应的表属性加上one的表的主码，形成一个新表，one对应的表不用添加新属性**==

* many-to-many：is represented as
  * a schema with attributes for the ==**primary keys of the two participating entity sets**==
  * any ==**descriptive attributes of the relationship set**==
  * ![image-20230523082105700](C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230523082105700.png)

* many-to-one：can be represented
  * ==**by adding the primary key of the "one" side to the "many" side**==
  * ![image-20230523083303462](C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230523083303462.png)

* one-to-one
  * **==extra attribute can be added to either side==**
  * ![image-20230523083457178](C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230523083457178.png)

* <img src="C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230523091622960.png" alt="image-20230523091622960" style="zoom:80%;" />



==**Placement of Relationship Attributes（关系属性的放置）**==

* Attributes of a one-to-many relationship set can be repositioned to the entity set on the many side：
  * ==**在一对多（1：N）的关系中，关系集的属性可以被放置在“多”这一边的实体集中**==
  * 假设==**我们有一个“教师”和“学生”的一对多关系，关系集的属性是“指导日期”。**==在这种情况下，我们==**可以将“指导日期”这个属性放在“学生”实体集中**==，因为==**一个教师可以指导多个学生，每个学生的指导日期可能不同。**==
* Attributes of a one-to-one relatioship set can be repositioned to either side
  * ==**在一对一（1：1）的关系中，关系集的属性可以被放置在任何一边的实体集中**==
  * 假设==**我们有一个“人”和“护照”的一对一关系，关系集的属性是“签发日期”。**==在这种情况下，==**我们可以将“签发日期”这个属性放在“人”实体集中，也可以将它放在“护照”实体集中，**==因为每个人只有一个护照，每个护照也只属于一个人，所以签发日期可以归属到任一实体集中。



==**Representing Composite Attributes（表示复合属性）**==

![image-20230523091036881](C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230523091036881.png)



==**Representing Multivalued Attributes（表示多值属性）**==

![image-20230523091303820](C:\Users\yi'k\AppData\Roaming\Typora\typora-user-images\image-20230523091303820.png)

