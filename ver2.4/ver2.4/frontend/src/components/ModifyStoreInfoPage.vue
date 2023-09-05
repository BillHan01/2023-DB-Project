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
            用户信息修改（商户）
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
            <el-form-item label="用户ID">
              <el-input v-model="basicInfo.user_ID" disabled></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="电话号码">
              <el-input v-model="basicInfo.user_phone" disabled></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="密码">
              <el-input v-model="basicInfo.user_password" type="password" disabled></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="商户名">
              <el-input v-model="userInfo.sto_name"></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="类别">
              <el-select v-model="userInfo.categories" multiple clearable placeholder="Select" filterable allow-create>
                <el-option v-for="item in categories" :key="item" :label="item" :value="item"></el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="地址">
              <el-input v-model="addressInput"></el-input>
              <el-button @click="searchLocation">搜索地址</el-button>
              <div id="baiduMap" style="width:100%; height: 300px; margin-top:10px;"></div>
            </el-form-item>
        <el-form-item label="商家简介">
          <el-input v-model="userInfo.sto_introduction"></el-input>
        </el-form-item>
        <el-form-item label="商家执照图片">
          <el-upload v-model:file-list='fileList' class="upload-demo"
            action="https://run.mocky.io/v3/9d059bf9-4660-45f2-925d-ce80ad6c4d15" :auto-upload="false">
            <template #trigger>
              <el-button type="primary">select file</el-button>
            </template>
          </el-upload>
        </el-form-item>
        <el-form-item label="商家图片">
          <div v-for="(pic, index) in storedImages" :key="index">
            <img :src="pic.fullUrl" alt="Stored Image" width="100" height="100" />
            <el-button type="danger" @click="deleteStoredImage(index)">删除</el-button>
          </div>
          <el-upload v-model:file-list='fileList1' class="upload-demo"
            action="https://run.mocky.io/v3/9d059bf9-4660-45f2-925d-ce80ad6c4d15" :auto-upload="false">
            <template #trigger>
              <el-button type="primary">select file</el-button>
            </template>
          </el-upload>
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
const basicInfo = ref({});
const userInfo = ref({});
const categories = ref([]);
const fileList = ref([]);
const fileList1 = ref([]);
const storedImages = ref([]);
/*地址修改此处*/ 
const address='http://localhost:5000/api/';
  
  /*           */
//加入百度地图组件
/* eslint-disable */
let map: BMapGL.Map;
let geoc: BMapGL.Geocoder;
const addressInput = ref('');     //地址搜索框绑定变量


onMounted(async () => {
  //const sto_ID = route.query.id;
  const sto_ID = sessionStorage.getItem('sto_id');

  const basic_response = await axios.get(address+`getinformation/user`, { params: { user_ID:sto_ID } });
  if (basic_response.status === 200) {
    basicInfo.value = basic_response.data;
    console.log(basicInfo.value);
    } else {
    console.error(`Error: HTTP status code ${basic_response.status}`);
  }


    map = new BMapGL.Map("baiduMap"); 
    geoc = new BMapGL.Geocoder();
    //const map = new BMapGL.Map("baiduMap"); 
    const point = new BMapGL.Point(116.404, 39.915);  // 创建点坐标
    map.centerAndZoom(point, 15);                     // 初始化地图，设置中心点坐标和地图级别
    map.enableScrollWheelZoom(true);                  // 开启鼠标滚轮缩放
    addressInput.value = basicInfo.value.user_address;
    // 创建地址解析器实例
    const myGeo = new BMapGL.Geocoder();
    // 将地址解析结果显示在地图上，并调整地图视野
    myGeo.getPoint(basicInfo.value.user_address, function(point) {
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
        basicInfo.value.user_address = addressInput.value;
        var marker = new BMapGL.Marker(pt);  // 创建标注
        map.clearOverlays(); // 清除所有覆盖物
        map.addOverlay(marker); // 将标注添加到地图中
        //marker.enableDragging(); // 可拖拽
      });
    });
  
  const response = await axios.get(address+`getinformation/store`, { params: { sto_ID: sto_ID } });
  if (response.status === 200) {
    userInfo.value = response.data;
  } else {
    console.error(`Error: HTTP status code ${response.status}`);
  }
    // 获取已有的商家图片
  const imgResponse = await axios.get(address+`getinformation/storeimg`, { params: { sto_ID: sto_ID } });
  if (imgResponse.status === 200) {
    storedImages.value = imgResponse.data.imageURL.map(pic => ({
      fullUrl: 'http://localhost:5000\\' + pic,
      relativePath: pic
    }));
    console.log(storedImages.value);
  } else {
    console.error(`Error: HTTP status code ${imgResponse.status}`);
  }


  const response_categoty = await axios.get(address+`category/getcategories`);
    if (response_categoty.status === 200) {
      categories.value = response_categoty.data.categorylist;
    } else {
      console.error(`Error: HTTP status code ${response_categoty.status}`);
    }
});

const deleteStoredImage = async (imageIndex) => {
  try {
    const relativePathToDelete = storedImages.value[imageIndex].relativePath;
    const response = await axios.post(address+'modify/delstoreimg', {
      sto_ID: basicInfo.value.user_ID, 
      url: relativePathToDelete
    }, {
      params: {
        sto_ID: basicInfo.value.user_ID
      }
    });

    if (response.status === 200 && response.data.message === 'success') {
      // 删除图片在storedImages中的引用
      storedImages.value.splice(imageIndex, 1);
      alert('图片删除成功');
    } else {
      alert('删除失败');
    }
  } catch (error) {
    console.error(error);
  }
};

const goBack = () => {
      router.push({ path: '/store' });
  };

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
          basicInfo.value.user_address = addressInput.value;
          //form.sto_lng = pt.lng.toString();
          //form.sto_lat = pt.lat.toString();
        });

      } else {
        ElMessage.error('未找到相关地址！');
      }
    });

    local.search(addressInput.value);
  };

const modifyUser = async () => {
  try {
    if (fileList1.value.length + storedImages.value.length < 1 || fileList1.value.length + storedImages.value.length > 9) {
    alert('商家图片数量必须在1-9张之间！');
    return;
  }
    const userResponse = await axios.post(address+'modify/user', {
        user_ID: basicInfo.value.user_ID,
        user_phone: basicInfo.value.user_phone,
        user_password: basicInfo.value.user_password,
        user_address: basicInfo.value.user_address,
      });

    const formData = new FormData();

    fileList.value.forEach((file) => {
        formData.append('sto_licenseImg', file.raw); // 将文件添加到FormData中
    });


    fileList1.value.forEach((file) => {
        formData.append('sto_picture', file.raw); // 将文件添加到FormData中
    });


    formData.append('sto_ID', userInfo.value.sto_ID);
    formData.append('sto_name', userInfo.value.sto_name);
    formData.append('sto_introduction', userInfo.value.sto_introduction);
    userInfo.value.categories.forEach((category: string) => {
      formData.append('categories', category);
    });

    const storeResponse = await axios.post(address+'modify/store', formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });

    if (userResponse.data.message === 'success' && storeResponse.data.message === 'success') {
      alert('修改成功');
      router.push({ name: 'UserInfoPage' });
    } else {
      alert('修改失败');
    }
  } catch (error) {
    console.error(error);
  }
};

</script>
