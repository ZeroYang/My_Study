--https://leetcode-cn.com/problems/second-highest-salary/
-- 176. 第二高的薪水
-- SQL架构
-- 编写一个 SQL 查询，获取 Employee 表中第二高的薪水（Salary） 。

-- +----+--------+
-- | Id | Salary |
-- +----+--------+
-- | 1  | 100    |
-- | 2  | 200    |
-- | 3  | 300    |
-- +----+--------+
-- 例如上述 Employee 表，SQL查询应该返回 200 作为第二高的薪水。如果不存在第二高的薪水，那么查询应返回 null。

-- +---------------------+
-- | SecondHighestSalary |
-- +---------------------+
-- | 200                 |
-- +---------------------+

# Write your MySQL query statement below
select max(Salary) SecondHighestSalary
from Employee
where Salary < (select max(Salary) from Employee);

-- 首先考虑选取最高工资的， 然后再选取次高， 其中用到了嵌套