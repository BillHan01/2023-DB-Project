## 数据库设计 E-R绘制

以下设计中不包含时间段，所有时间属性均为时间戳。

图像存储相对路径

实体集：

- 用户
  - **用户ID**、电话号码（作为账号，不可重复）、登录密码、地理位置（复合，{国家，省份，城市，区/县，路，号}）、登录状态（登录、未登录）、余额、注册时间、用户类型(顾客、商家)
  - 顾客与商家由用户完全不相交特化而来，实际数据库中建议分别用三个表存储，参考教材（申诉与评论表的需求）

- 顾客：
  - 昵称、购买喜好（多值复合{商品类别、权值}）、个人备注、支付密码、账号状态（正常、封禁）
- 商家
  - 商家名称、商家简介（管理员可修改，管理员备注内容融合在这里）、营业执照图像、***店铺图像（多值，非空）***、***主营类别(多值)***、商家状态（待审核、正常、封禁）
- 商品
  - **商品ID**、商品名称、商品简介（管理员可修改，管理员备注内容融合在这里）、***商品图像（多值，非空）***、***商品类别(多值)***、原价、到期日期、***价格曲线（多值）***、上传时间、商品余量、评分
- 订单
  - **订单ID**、购买数量、支付金额、购买时间、核销码、用户备注、订单状态(待核销、已完成、已退款)、评分
  - 部分特化子类：已退款订单：退款原因、退款时间、退款金额（派生属性）
- 评论
  - **评论ID**、父评论ID、评论内容、评论时间
- 申诉
  - **申诉ID**、申诉事项（针对评论、商品、商家、顾客）、申述描述、***截图（可空、多值）***、申诉用户ID(可空)、申诉商品ID(可空)、申诉评论ID(可空)
- 公告
  - **公告ID**、发布时间、内容、显示状态（bool）

- 管理员
  - **管理员ID**、账号（不可重复）、密码、登录状态


联系集：

- 下单 订单-顾客  多对一
- 购买 订单-商品  多对一
- 销售 商品-商家  多对一
- 反馈 评论-商品 多对一
- 发布 评论-用户 多对一
- 投诉 申诉-用户 多对一
- 被投诉 申诉-用户 0...1---0...1


- 被投诉 申诉-评论 0...1---0...1

- 被投诉 申诉-商品 0...1---0...1

  > 注意，上面三个被投诉联系集，在实际数据库中，会去除，分别作为申诉用户ID、申诉商品ID、申诉评论ID三个值存在。他们外码依赖于用户、商品、评论表，但同时，这三个值是可空的（根据申诉事项不同，这三个属性分别取用）。我查到oracle外键是可以为空的，表示其值不确定。

- 发布 公告-商家 多对一

  > 上述所有联系集在转化为实际数据库时，都可以合并到“一”实体集中，用外码约束表示

- 浏览记录：商品-顾客 多对多

  - 浏览起始时间、浏览终止时间、是否购买

- 收藏：顾客-商品 多对多 

  - 收藏时间

- 聊天记录： 顾客-商家 多对多
  - 内容（一个字符串）、时间戳、发出者（顾客，商家）

