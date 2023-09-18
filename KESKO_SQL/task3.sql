-- Переименовал таблицу Routes в RoutesTable. Добавил в неё новый столбец Route_Id - Id маршрута. 
-- Теперь все строки (все поездки) с одинаковым Route_Id cоставляют маршрут.
-- Одна машина проехала от склада по всем магазинам маршрута (за один день)
--   Но если маленькая машина на длинном маршруте, то она возвращатеся на склад, догружается
--   и снова едет по том уже маршруту (одна такая ситуация есть и обрабатывается).

-- В БД 3 маршрута и 5 машин разной грузоподъёмности (от 1 до 10 тонн).
-- Все машины по разу проходят все маршруты

-- Маршрут1: D1 - S4 - S5 - S6 (4)
-- Маршрут2: D2 - S7 - S8 - S9 - S10 (5)
-- Маршрут3: D3 - S11 - S12 - S13 - S14 - S15 - S16 (7)
-- Всего: (4+5+7) * 5 = 80 (поездок) строк и 15 маршрутов.
-------------------------------------------------------------------------------------

-- Задание
-- найти топ-3 неэффективных маршрутов и топ-3 неэффективных машин по каждому из критериев.


-- 1.1. Недозагруженность машины, т.е. 
-- отношение «загрузка машины/грузоподъемность» после выезда со склада в начале маршрута

--топ-3 неэффективных машин
select  top(3) round(cast(Load as float) / cast(Capacity as float), 3), Car_Id
from RoutesTable
join Points on RoutesTable.Point_Id = Points.Id
join Cars on RoutesTable.Car_Id = Cars.Id
where TypePoint_Id = 'D'
group by Car_Id, cast(Load as float) / cast(Capacity as float)
order by cast(Load as float) / cast(Capacity as float)

--топ-3 неэффективных маршрутов 
--select top(3) round(cast(Load as float) / cast(Capacity as float), 3), Route_Id
--from RoutesTable
--join Points on RoutesTable.Point_Id = Points.Id
--join Cars on RoutesTable.Car_Id = Cars.Id
--where TypePoint_Id = 'D'
--group by Route_Id, cast(Load as float) / cast(Capacity as float)
--order by cast(Load as float) / cast(Capacity as float)


-------------------------------------------------------------------------------------
-- 1.2. Размер остатка в машине после посещения всех магазинов на маршруте

-- В данном случае неэффективные маршруты и машины доступны в одном запросе
select top(3) Route_Id, Car_Id, sum(Load) as RemainingWeight  --RemainingWeight - Вес остатка груза
from RoutesTable
group by Route_Id, Car_Id
order by RemainingWeight DESC;


-------------------------------------------------------------------------------------
-- 1.3. Найти самую быструю машину или доказать, что по представленным данным
-- это невозможно сделать
