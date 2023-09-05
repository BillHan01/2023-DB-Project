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
            个人信息修改（顾客）
          </h3>
        </el-col>
        <el-col :span="20" style="display: flex; justify-content: flex-end; align-items: center;">
          <el-link icon="el-icon-arrow-left" @click="goBack" style="color: aliceblue; font-size: large; margin-left: 15px;">返回主页</el-link>
        </el-col>
      </el-row>
    </el-header>
    <el-main>
      <el-form :model="userInfo" ref="form" @submit.prevent="modifyUser" style="max-width: 800px; position: relative; margin: auto;">
        <el-row :gutter="25">
          <el-col :span="12">
            <el-form-item label="账号">
              <el-input v-model="userInfo.user_ID" disabled></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="电话号码">
              <el-input v-model="userInfo.user_phone"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="密码">
              <el-input v-model="userInfo.user_password" type="password"></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="用户昵称">
              <el-input v-model="cusInfo.cus_nickname"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
            <el-form-item label="喜好种类">
              <el-select v-model="cusInfo.cus_loves" multiple clearable placeholder="Select" filterable allow-create>
                <el-option v-for="item in categories" :key="item" :label="item" :value="item"></el-option>
              </el-select>
            </el-form-item>
        </el-row>
        <el-form-item label="地址">
              <el-input v-model="addressInput" @keyup.enter="searchLocation"></el-input>
              <div id="baiduMap" style="width:100%; height: 300px; margin-top:10px;"></div>
            </el-form-item>
        <el-form-item label="个人备注">
          <el-input v-model="cusInfo.cus_notes"></el-input>
        </el-form-item>
        <el-button type="primary" native-type="submit">提交修改</el-button>
      </el-form>
    </el-main>
  </el-container>
</template>

  
  <script lang="ts" setup>
  import { ref, onMounted } from 'vue';
  import axios from 'axios';
  import { useRoute, useRouter } from 'vue-router';
  
  const route = useRoute();
  const router = useRouter();
  const userInfo = ref({});
  const cusInfo=ref({})
  const categories = ref([]);  
  /*地址修改此处*/ 
  const address='http://localhost:5000/api/'
    /*           */

  //加入百度地图组件
  /* eslint-disable */
  let map: BMapGL.Map;
  let geoc: BMapGL.Geocoder;
  const addressInput = ref('');     //地址搜索框绑定变量

  onMounted(async () => {
    const user_ID = sessionStorage.getItem('cus_id') as string;
    const cus_ID=sessionStorage.getItem('cus_id') as string
    console.log(sessionStorage.getItem('cus_id'))
    const response = await axios.get(address+`getinformation/user`, { params: { user_ID:user_ID } });
    if (response.status === 200) {
      userInfo.value = response.data;
    } else {
      console.error(`Error: HTTP status code ${response.status}`);
    }

    map = new BMapGL.Map("baiduMap"); 
    geoc = new BMapGL.Geocoder();
    //const map = new BMapGL.Map("baiduMap"); 
    const point = new BMapGL.Point(116.404, 39.915);  // 创建点坐标
    map.centerAndZoom(point, 15);                     // 初始化地图，设置中心点坐标和地图级别
    map.enableScrollWheelZoom(true);                  // 开启鼠标滚轮缩放
    addressInput.value = userInfo.value.user_address;
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

    // 添加地图点击事件监听
    map.addEventListener("click", function (e: any) {
      const pt = e.latlng;
      //alert('点击的经纬度：' + e.latlng.lng + ', ' + e.latlng.lat);
      geoc.getLocation(pt, function (rs: any) {
        console.log('Complete geocoding result:', rs);
        const addComp = rs.addressComponents;
        addressInput.value = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
        //alert(form.user_address);
        //form.sto_lng = pt.lng.toString();
        //form.sto_lat = pt.lat.toString();
        //alert('点击的经纬度：' + pt.lng.toString() + ', ' + pt.lat.toString());
        userInfo.value.user_address = addressInput.value;
        var marker = new BMapGL.Marker(pt);  // 创建标注
        map.clearOverlays(); // 清除所有覆盖物
        map.addOverlay(marker); // 将标注添加到地图中
        //marker.enableDragging(); // 可拖拽
      });
    });

    const response_cus = await axios.get(address+`getinformation/customer`, { params: { cus_ID } });
    if (response_cus.status === 200) {
      cusInfo.value = response_cus.data;
    } else {
      console.error(`Error: HTTP status code ${response_cus.status}`);
    }

    const response_categoty = await axios.get(address+`category/getcategories`);
    if (response_categoty.status === 200) {
      categories.value = response_categoty.data.categorylist;
    } else {
      console.error(`Error: HTTP status code ${response_categoty.status}`);
    }
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
          addressInput.value = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
          userInfo.value.user_address = addressInput.value;
          //form.sto_lng = pt.lng.toString();
          //form.sto_lat = pt.lat.toString();
        });

      } else {
        ElMessage.error('未找到相关地址！');
      }
    });

    local.search(addressInput.value);
  };

  const goBack = () => {
      router.push({ path: '/home' });
  };

  const modifyUser = async () => {
    try {
      const userResponse = await axios.post(address+'modify/user', {
        user_ID: userInfo.value.user_ID,
        user_phone: userInfo.value.user_phone,
        user_password: userInfo.value.user_password,
        user_address: userInfo.value.user_address,
      });
  
      const customerResponse = await axios.post(address+'modify/customer', {
        cus_ID: cusInfo.value.cus_ID,
        cus_nickname: cusInfo.value.cus_nickname,
        cus_notes: cusInfo.value.cus_notes,
        cus_payPassword: cusInfo.value.cus_payPassword,
        cus_category: cusInfo.value.cus_loves,
      });
  
      if (userResponse.data.message === 'success' && customerResponse.data.message === 'success') {
        alert('修改成功');
        router.push({ name: 'UserInfoPage'});
      } else {
        alert('修改失败');
      }
    } catch (error) {
      console.error(error);
    }
  };

  const editing = ref(false);

    const startEditing = () => {
    userInfo.value.cus_category = [];  // Clear the categories
    editing.value = true;
    };
  </script>
  