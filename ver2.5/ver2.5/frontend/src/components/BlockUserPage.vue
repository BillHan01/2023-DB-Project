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
                    抱歉，您的账号已经被封禁！
                  </h3>
              </el-col>
              <el-col :span="20" style="display: flex; justify-content: flex-end; align-items: center;">
                  <el-link @click="goBack" style="color: aliceblue; font-size: large; margin-left: 15px;">返回登录界面</el-link>
              </el-col>
          </el-row>
      </el-header>
      <el-main>
        <div class="form-layout">
            <div style="margin-top: 20px;">
            <p>非常抱歉，您的账号已经被永久封禁。您可以选择进行提现操作。您的账户余额为: ¥{{ userInfo.user_balance }}</p>
            <el-button @click="withdrawNow" type="primary">提现</el-button>
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

  onMounted(async () => {
     //user_ID.value = route.query.id as string;
     if(sessionStorage.getItem('user_id')!=null){
       user_ID.value = sessionStorage.getItem('user_id') as string;
       console.log("被封禁的账号为"+user_ID.value);
    }
    console.log("尊敬的用户"+user_ID.value)
    try {
      const response = await axios.get('/api/getinformation/user', { params: {user_ID:user_ID.value } });
      if (response.status === 200) {
        userInfo.value = response.data;
        console.log(userInfo.value);
      } else {
        console.error(`Error: HTTP status code ${response.status}`);
      }
      console.log(userInfo.value.user_type)
    } catch (error) {
      console.error(error);
    }
    
  });

    const displayUserType = computed(() => {
    return userInfo.value.user_type === '0' ? '顾客' : '商户';
    });

  const router = useRouter();
  
  const goBack = () => {
    router.push({ path: '/login' });
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