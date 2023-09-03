<template>
  <div>
  <h1 class="header">用户注册</h1>
</div>
  <el-form :model="form" 
           @submit.prevent="register"
           label-width="100px" 
           style="max-width: 460px;position: relative;margin: auto;"
           label-position="right">
    <el-form-item label="电话号码" prop="user_phone" :validate-status="phoneStatus" :error="phoneError">
      <el-input v-model="form.user_phone" @blur="validatePhone"></el-input>
    </el-form-item>
    <el-form-item label="密码" prop="user_password">
      <el-input type="password" v-model="form.user_password"></el-input>
    </el-form-item>
    <el-form-item label="确认密码" prop="confirm_password" :validate-status="confirmStatus" :error="confirmError">
      <el-input type="password" v-model="form.confirm_password" @blur="validateConfirm"></el-input>
    </el-form-item>
    <el-form-item label="地址" prop="user_address" :validate-status="addressStatus" :error="addressError">

      <el-input v-model="addressInput" @keyup.enter="searchLocation"></el-input>
      <div id="baiduMap" style="width:100%; height: 300px; margin-top:10px;"></div>

    </el-form-item>
    <el-form-item label="用户类型" prop="user_type">
      <el-select v-model="form.user_type" placeholder="请选择用户类型">
        <el-option label="顾客" value="0"></el-option>
        <el-option label="商户" value="1"></el-option>
      </el-select>
    </el-form-item>
    <el-form-item>
      <el-button type="primary" native-type="submit">确认注册</el-button>
    </el-form-item>
  </el-form>
</template>

<script lang="ts" setup>
import { reactive, ref, onMounted } from 'vue';
import axios from 'axios';
import { useRouter } from 'vue-router';
import { ElMessage } from 'element-plus';
import options from '@/dist/pcas.json';

const router = useRouter();
const form = reactive({
user_phone: '',
user_password: '',
confirm_password: '',
user_address: '',
user_type: '',
});

//加入百度地图组件
/* eslint-disable */
let map: BMapGL.Map;
let geoc: BMapGL.Geocoder;
const addressInput = ref('');     //地址搜索框绑定变量

onMounted(() => {
  map = new BMapGL.Map("baiduMap"); 
  geoc = new BMapGL.Geocoder();
  //const map = new BMapGL.Map("baiduMap"); 
  const point = new BMapGL.Point(116.404, 39.915);  // 创建点坐标
  map.centerAndZoom(point, 15);                     // 初始化地图，设置中心点坐标和地图级别
  map.enableScrollWheelZoom(true);                  // 开启鼠标滚轮缩放

  //const geoc = new BMapGL.Geocoder();  

  // 添加地图点击事件监听
  map.addEventListener("click", function (e: any) {
    const pt = e.latlng;
    //alert('点击的经纬度：' + e.latlng.lng + ', ' + e.latlng.lat);
    geoc.getLocation(pt, function (rs: any) {
      console.log('Complete geocoding result:', rs);
      const addComp = rs.addressComponents;
      form.user_address = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
      //alert(form.user_address);
      //form.sto_lng = pt.lng.toString();
      //form.sto_lat = pt.lat.toString();
      //alert('点击的经纬度：' + pt.lng.toString() + ', ' + pt.lat.toString());
      addressInput.value = form.user_address;   // 更新输入框的值
      var marker = new BMapGL.Marker(pt);  // 创建标注
      map.clearOverlays(); // 清除所有覆盖物
      map.addOverlay(marker); // 将标注添加到地图中
      //marker.enableDragging(); // 可拖拽
    });
  });
});


const searchLocation = () => { // 搜索地点
  const local = new BMapGL.LocalSearch(map, {
    renderOptions: { map: map, autoViewport: true, selectFirstResult: false },
    pageCapacity: 8,
  });

  // 搜索回调
  local.setSearchCompleteCallback(results => {
    if (local.getStatus() === BMAP_STATUS_SUCCESS) {
      const pt = results.getPoi(0).point;
      //form.sto_locationPoint = `${pt.lng},${pt.lat}`;

      // 更新输入框地址
      geoc.getLocation(pt, rs => {
        const addComp = rs.addressComponents;
        form.user_address = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
        addressInput.value = form.user_address;
        //form.sto_lng = pt.lng.toString();
        //form.sto_lat = pt.lat.toString();
      });

    } else {
      ElMessage.error('未找到相关地址！');
    }
  });

  local.search(addressInput.value);
};

const phoneStatus = ref('');
const phoneError = ref('');
const confirmStatus = ref('');
const confirmError = ref('');
const addressStatus = ref('');
const addressError = ref('');

const optionProps={
  value:'name',
  label:'name',
  children:'children'
}



const phoneReg = /^1[3-9]\d{9}$/;   //正则表达式，暂时先检测中国号码。。。。
/*地址修改此处*/ 
const address='http://localhost:5000/api/'
  /*           */


const handleChange=()=>{
    console.log(form.user_address)
}

const validatePhone = () => {
  if (!phoneReg.test(form.user_phone)) {
    phoneStatus.value = 'error';
    phoneError.value = '电话号码格式不正确';
  } else {
    phoneStatus.value = 'success';
    phoneError.value = '';
  }
};

const validateAddress = () => {
  if (!form.user_address || form.user_address.length === 0) {
    addressStatus.value = 'error';
    addressError.value = '请选择地址';
  } else {
    addressStatus.value = 'success';
    addressError.value = '';
  }
};

const validateConfirm = () => {
  if (form.user_password !== form.confirm_password) {
    confirmStatus.value = 'error';
    confirmError.value = '两次输入的密码不一致';
  } else {
    confirmStatus.value = 'success';
    confirmError.value = '';
  }
};

const register = async () => {
  validatePhone();
  validateAddress();
  validateConfirm();
  if (phoneStatus.value === 'error' || addressStatus.value === 'error' || confirmStatus.value === 'error') {
    return;
  }
  try {
    const registerForm = {
      user_phone: form.user_phone,
      user_password: form.user_password,
      user_address: form.user_address,
      user_type: form.user_type.toString(),
    };
    console.log(registerForm);
    const response = await axios.post(address+'register', registerForm);
    console.log(response.data);
    if (response.status === 200) {
      const data = response.data;
      if (data.message === 'success') {
        localStorage.setItem('user_ID', data.user_ID); // 保存 user_ID 到 localStorage
        if (data.user_type === '0') {
          ElMessage({  // 使用 ElMessage
            message: '注册成功，正在跳转登录页面...',
            type: 'success',
          });
          sessionStorage.removeItem('id');
          sessionStorage.setItem('id',data.user_ID)
          router.push({name:'customerregister'});
        } else if (data.user_type === '1') {
          ElMessage({  // 使用 ElMessage
            message: '注册成功，正在跳转商户注册页面...',
            type: 'success',
          });
          // 注册成功，如果是商家用户，跳转到商户注册界面
          sessionStorage.removeItem('id');
          sessionStorage.setItem('id',data.user_ID)
          router.push({ name: 'storeregister'});
        }
      } else {
        // 如果不是 'success'，那么这是一个错误消息
        ElMessage({  // 使用 ElMessage
          message: data.message,
          type: 'error',
        });
      }
    } else {
      console.error(`Error: HTTP status code ${response.status}`);
    }
  } catch (error) {
    console.error(error);
  }
};


</script>

<style>
.header{
  display: flex;
  justify-content: center;
  align-items: center;
  
}
</style>