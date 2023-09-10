DECLARE @type int, @client int, @date_start DATE, @date_stop DATE;
SET @type = 1;
SET @client = 1;
SET @date_start = PLACE_FOR_START_DATA;
SET @date_stop = PLACE_FOR_STOP_DATA;



;with timePeriods as ( 
 select
  id, ContractType_id, Client_Id, DateFrom, DateTo,
  ROW_NUMBER() over (order by DateFrom, DateTo) as rn                    
 from Contracts
 where (ContractType_id = @type and Client_Id = @client AND DateFrom between @date_start and @date_stop) or (ContractType_id = @type and Client_Id = @client AND DateTo between @date_start and @date_stop)
), cte as (
 select 
  id, ContractType_id, Client_Id, DateFrom, DateTo, rn, 1 as GroupId
 from timePeriods
 where rn = 1 

 union all

 select 
  p2.id,
  p1.ContractType_id,                                                     
  p1.Client_Id,
  case
  when (p1.DateFrom between p2.DateFrom and p2.DateTo) then p2.DateFrom
  when (p2.DateFrom between p1.DateFrom and p1.DateTo) then p1.DateFrom
  when (p1.DateFrom < p2.DateFrom and p1.DateTo > p2.DateTo) then p1.DateFrom
  when (p1.DateFrom > p2.DateFrom and p1.DateTo < p2.DateTo) then p2.DateFrom
  else p2.DateFrom
  end as DateFrom,

  case
  when (p1.DateTo between p2.DateFrom and p2.DateTo) then p2.DateTo
  when (p2.DateTo between p1.DateFrom and p1.DateTo) then p1.DateTo
  when (p1.DateFrom < p2.DateFrom and p1.DateTo > p2.DateTo) then p1.DateTo
  when (p1.DateFrom > p2.DateFrom and p1.DateTo < p2.DateTo) then p2.DateTo
  else p2.DateTo
  end as DateTo,

  p2.rn,
  case when
  (p1.DateFrom between p2.DateFrom and p2.DateTo) or
  (p1.DateTo between p2.DateFrom and p2.DateTo) or
  (p1.DateFrom < p2.DateFrom and p1.DateTo > p2.DateTo) or
  (p1.DateFrom > p2.DateFrom and p1.DateTo < p2.DateTo)
  then
  p1.GroupId
  else
  (p1.GroupId+1)
  end as GroupId
 from cte p1 
 inner join timePeriods p2
  on p1.ContractType_id = p2.ContractType_id and (p1.rn+1) = p2.rn                               
)


select
GroupId, min(DateFrom) DateFrom, max(DateTo) DateTo,      
string_agg(id,',') within group (order by id) as overlapList
from cte

group by GroupId 
HAVING COUNT(*) = 1
order by GroupId 


