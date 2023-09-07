<template>
    <el-container>
      <el-header style="background-color: #337ecc;">
          <el-row justify="space-between">
              <el-col :span="4">
                  <h3 style="  
                    color:aliceblue;
                    text-overflow:ellipsis;
                    white-space:nowrap;
                    overflow:hidden;">
                    个人信息
                  </h3>
              </el-col>
              <el-col :span="20" style="display: flex; justify-content: flex-end; align-items: center;">
                  <el-link icon="el-icon-edit" @click="modifyUserInfo" style="color: aliceblue; font-size: large; margin-left: 15px;">修改信息</el-link>
                  <el-link icon="el-icon-plus" @click="rechargeNow" style="color: aliceblue; font-size: large; margin-left: 15px;">充值</el-link>
                  <el-link icon="el-icon-minus" @click="withdrawNow" style="color: aliceblue; font-size: large; margin-left: 15px;">提现</el-link>
                  <el-link icon="el-icon-arrow-left" @click="goBack" style="color: aliceblue; font-size: large; margin-left: 15px;">返回主页</el-link>
              </el-col>
          </el-row>
      </el-header>
      <el-main>
        <el-form :model="userInfo" style="max-width: 800px; position: relative; margin: auto;">
          <el-row :gutter="25">
            <el-col :span="12">
              <el-form-item label="用户ID">
                <el-input v-model="userInfo.user_ID" disabled></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="电话号码">
                <el-input v-model="userInfo.user_phone" disabled></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="密码">
                <el-input v-model="userInfo.user_password" type="password" disabled></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="余额">
                <el-input v-model="userInfo.user_balance" disabled></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="注册时间">
                <el-input v-model="userInfo.user_regTime" disabled></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="用户类型">
                <el-input v-model="displayUserType" disabled></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-form-item label="地址">
            <el-input v-model="userInfo.user_address" disabled></el-input>
            <div id="baiduMap" style="width:100%; height: 300px; margin-top:10px;"></div>
          </el-form-item>
          <!-- 若是顾客，显示顾客相关的表单项 -->
          <div v-if="displayUserType === '顾客'">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="昵称">
                  <el-input v-model="userInfo.cus_nickname" disabled></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="备注">
                  <el-input v-model="userInfo.cus_notes" disabled></el-input>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="用户状态">
                    <el-input v-model="displayUserState" disabled></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="用户喜好">
                    <el-select v-model="userInfo.cus_loves" multiple disabled>
                    <el-option
                        v-for="(item, index) in userInfo.cus_loves"
                        :key="index"
                        :label="item"
                        :value="item"
                    ></el-option>
                    </el-select>
                </el-form-item>
              </el-col>
            </el-row>
          </div>
          <!-- 若是商户，显示商户相关的表单项 -->
          <div v-else-if="displayUserType === '商户'">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="商店名称">
                  <el-input v-model="userInfo.sto_name" disabled></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="商店介绍">
                  <el-input v-model="userInfo.sto_introduction" disabled></el-input>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="商店状态">
                    <el-input v-model="displayStoreState" disabled></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="主营类型">
                    <el-select v-model="userInfo.categories" multiple disabled>
                    <el-option
                        v-for="(item, index) in userInfo.categories"
                        :key="index"
                        :label="item"
                        :value="item"
                    ></el-option>
                    </el-select>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row :gutter="20">
              <el-form-item label="商店营业执照">
                <el-image style="width: 100px; height: 100px" :src="image" />
              </el-form-item>
            </el-row>
            <el-row :gutter="20">
              <el-form-item label="商店图片">
                <div v-for="(pic, index) in storePictures" :key="index">
                  <el-image :src="pic" style="width: 100px; height: 100px; margin: 5px;"></el-image>
                </div>
              </el-form-item>
            </el-row>
          </div>
            <div v-if="showRecharge" class="recharge-overlay">
              <div class="recharge-modal">
                <h3>充值</h3>
                <el-input v-model="recharge_amount" placeholder="请输入充值金额"></el-input>
                <div class="buttons">
                  <el-button @click="showRecharge = false">取消</el-button>
                  <el-button type="primary" @click="confirmRecharge">确认支付</el-button>
                </div>
              </div>
            </div>
            <div v-if="showWithdraw" class="withdraw-overlay">
              <div class="withdraw-modal">
                <h3>提现</h3>
                <el-input v-model="withdraw_amount" placeholder="请输入提现金额"></el-input>
                <div class="buttons">
                  <el-button @click="showWithdraw = false">取消</el-button>
                  <el-button type="primary" @click="confirmWithdraw">确认提现</el-button>
                </div>
              </div>
            </div>          
        </el-form>
      </el-main>
    </el-container>
  </template>
  
  <script lang="ts" setup>
  /* eslint-disable */
  import { ref, onMounted, computed } from 'vue';
  import axios from 'axios';
  import { useRouter, useRoute } from 'vue-router';

  const route = useRoute();
  const userInfo = ref({});
  const image=ref('')
  const storePictures = ref([]);
  const user_ID = ref('');
  //const alipayBase = 'http://localhost:3000';
  const alipayBase = 'http://124.70.7.210:3000';
  // 充值提现相关
  function generateOrderId() {
    // 获取当前时间的时间戳，精确到秒
    const timestamp = Math.floor(Date.now() / 1000); 
    // 生成一个1000到9999之间的随机数
    const randomDigits = Math.floor(Math.random() * 9000) + 1000;
    return `${timestamp}${randomDigits}`;
  }
  const showRecharge = ref(false);
  const showWithdraw = ref(false)
  const recharge_amount = ref('');  // 用户充值的数量
  const withdraw_amount = ref('')    //用户提现的数量
  const rechargeNow = () => {
    showRecharge.value = true;
  }
  const withdrawNow = () => {
      showWithdraw.value = true
  }
  const confirmRecharge = () => {
      console.log('11111');
    // 使用 generateOrderId 函数生成一个唯一的orderId
    const orderId = generateOrderId();

    axios.post(alipayBase + '/api/payment', {
      orderId: orderId,
      price: recharge_amount.value,
      name: '充值',
      cus_id: user_ID.value,
    })
    .then((res) => {
      window.location.href = res.data.data.paymentUrl;
    });
    axios.post('/api/balance/setBalance', {
      cus_id: user_ID.value,
      balance: parseFloat(userInfo.value.user_balance) + parseFloat(recharge_amount.value),
    })
    .then((res) => {    
    });
  }
  const confirmWithdraw = () => {
    if (parseFloat(withdraw_amount.value) > parseFloat(userInfo.value.user_balance)) {
        alert('余额不足');
        return;  // 提前终止函数的执行
    }

    const outBizNo = generateOrderId(); // 这需要是一个动态生成的唯一值
    const orderTitle = '提现';
    const payeeInfo = { 
      identity: '2088722008198443', // 支付宝用户ID
      identity_type: 'ALIPAY_USER_ID' // 支付宝的会员ID
    };

    axios.post(alipayBase + '/api/withdraw', {
        out_biz_no: outBizNo,
        trans_amount: parseFloat(withdraw_amount.value).toFixed(2),
        biz_scene: 'DIRECT_TRANSFER',
        product_code: 'TRANS_ACCOUNT_NO_PWD',
        order_title: orderTitle,
        payee_info: payeeInfo,
    })
    .then((res) => {
        if (res.data.success) {
          axios.post('/api/balance/setBalance', {
            cus_id: user_ID.value,
            balance: parseFloat(userInfo.value.user_balance) - parseFloat(withdraw_amount.value),
          })
          .then((res) => {    
          });
          alert('提现成功');
          window.location.reload();  // 刷新页面
        } else {
          alert('提现失败: ' + res.data.errorMessage);
        }
    })
    .catch((error) => {
        console.error(error);
        alert('提现请求失败');
    });

    showWithdraw.value = false;
}
    

  //加入百度地图组件
  /* eslint-disable */
  let map: BMapGL.Map;
  /* eslint-disable */
  let geoc: BMapGL.Geocoder;

  onMounted(async () => {
     //user_ID.value = route.query.id as string;
     if(sessionStorage.getItem('sto_id')!=null){
       user_ID.value = sessionStorage.getItem('sto_id') as string;
    }
    if(sessionStorage.getItem('cus_id')!=null){
       user_ID.value = sessionStorage.getItem('cus_id') as string;
    }
    console.log("这是ID"+user_ID.value)
    try {
      const response = await axios.get('/api/getinformation/user', { params: {user_ID:user_ID.value } });
      if (response.status === 200) {
        userInfo.value = response.data;

        map = new BMapGL.Map("baiduMap"); 
        geoc = new BMapGL.Geocoder();
        //const map = new BMapGL.Map("baiduMap"); 
        const point = new BMapGL.Point(116.404, 39.915);  // 创建点坐标
        map.centerAndZoom(point, 15);                     // 初始化地图，设置中心点坐标和地图级别
        map.enableScrollWheelZoom(true);                  // 开启鼠标滚轮缩放

        // 创建地址解析器实例
        const myGeo = new BMapGL.Geocoder();
        // 将地址解析结果显示在地图上，并调整地图视野
        myGeo.getPoint(userInfo.value.user_address, function(point) {
          if (point) {
            map.centerAndZoom(point, 16);
            map.addOverlay(new BMapGL.Marker(point));
          } else {
            alert("您选择地址没有解析到结果!");
          }
        });
      } else {
        console.error(`Error: HTTP status code ${response.status}`);
      }

      console.log(userInfo.value.user_type)
      if (userInfo.value.user_type === '0') {
        const customerResponse = await axios.get('/api/getinformation/customer', { params: { cus_ID: user_ID.value } });
        if (customerResponse.status === 200) {
            Object.assign(userInfo.value, customerResponse.data);
        } else {
            console.error(`Error: HTTP status code ${customerResponse.status}`);
        }
      } else if (userInfo.value.user_type === '1') {
        const storeResponse = await axios.get('/api/getinformation/store', { params: { sto_ID: user_ID.value } });
        image.value='http://124.70.7.210:5000\\'+storeResponse.data.sto_licenseImg;
        if (storeResponse.status === 200) {
            Object.assign(userInfo.value, storeResponse.data);
            const storePicResponse = await axios.get('/api/getinformation/storeimg', { params: { sto_ID: user_ID.value } });
            storePictures.value = storePicResponse.data.imageURL.map(pic => 'http://124.70.7.210:5000\\' + pic);
        } else {
            console.error(`Error: HTTP status code ${storeResponse.status}`);
        }
      }
    } catch (error) {
      console.error(error);
    }
    
  });

    const displayUserType = computed(() => {
    return userInfo.value.user_type === '0' ? '顾客' : '商户';
    });

    const displayUserState = computed(() => {
    return userInfo.value.cus_state === '0' ? '封禁' : '正常';
    });

    const displayStoreState = computed(() => {
    return userInfo.value.sto_state === '0' ? '封禁' : '正常';
  });

  const router = useRouter();
  
  const goBack = () => {
    const userType = sessionStorage.getItem("user_type");
    if (userType === '1') {
      router.push({ path: '/store' });
    } else if (userType === '0') {
      router.push({ path: '/home' });
    } else {
      console.error("Invalid user type");
    }
  };
  const modifyUserInfo = () => {
  if (userInfo.value.user_type === '0') {
    router.push({ name: 'ModifyCustomerInfoPage' });
  } else {
    router.push({ name: 'ModifyStoreInfoPage'});
  }
};



    
  </script>
  

  <style scoped>
  .form-layout{
    display: flex;
    flex-direction:column;
    width:300px;
    margin-left: 620px;
    justify-content: center;
  }
  .header{
      display: flex;
      justify-content: center;
      align-items: center;
      
  }
  </style>