<!-- <template>
  <div>
    <el-table :data="tableData" stripe style="width: 100%">
    <el-table-column prop="attr" label="商品属性" width="180" />
    <el-table-column prop="info" label="信息" width="180" />
  </el-table>  
  <br>
    <div>
      <el-button type="primary" @click="goToHome">返回主页</el-button>
    </div>
  </div>
</template>

<script lang="ts" setup>
    import { ref, onMounted } from 'vue';
    import { useRouter } from 'vue-router';
    import axios from 'axios';
    const router = useRouter();
  
 //const add='http://124.70.7.210:5000';
  const add='http://localhost:5000';
    const commodityDetail = ref<any>(null); // 使用 ref 创建一个响应式对象

    const tableData = ref<any>([]);

    const goToHome = () => {
      router.push('home');
    }

    onMounted(async () => {
      try {
        console.log("这是ts版本")
        const response = await axios.get(`${add}/api/search/detail`, {
          params: {
            ID: 3,
          }
        })
        commodityDetail.value = response.data;
        tableData.value = [
          {
            attr: 'ID',
            info: commodityDetail.value.COM_ID,
          },
          {
            attr: '名称',
            info: commodityDetail.value.COM_NAME,
          },
          {
            attr: '简介',
            info: commodityDetail.value.COM_INTRODUCTION,
          },
          {
            attr: '原价',
            info: commodityDetail.value.COM_ORIPRICE,
          },
          {
            attr: '过期时间戳',
            info: commodityDetail.value.COM_EXPIRATIONDATE,
          },
          {
            attr: '上传时间',
            info: commodityDetail.value.COM_UPLOADDATE,
          },
          {
            attr: '库存量',
            info: commodityDetail.value.COM_LEFT,
          },
          {
            attr: '评分',
            info: commodityDetail.value.COM_RATING,
          },
          {
            attr: '商家ID',
            info: commodityDetail.value.STO_ID,
          },
    ]
      } catch (error) {
        console.log('An error occurred:', error);
      }
    });


</script> -->

<template>
  <div class="common-layout">
    <el-container>
        <el-header>
          <div style="text-align: center;">商品详情页面</div>
        </el-header>
        <el-container>
          <el-aside width="200px"></el-aside>
          <el-main>
            <div v-if="commodity">

  
    <el-divider></el-divider>

    <el-card class="box-card">
      
      <div style="width: 400px">
        <el-carousel height="200px" width="100px">
          <el-carousel-item v-for="(image, index) in commodity.com_images" :key="index">
            <!-- <img :src="`http://localhost:5000/commodityImage/${image}`" alt="商品图片" style="width: 100%; height: 100%;"> -->
            <img :src="`http://124.70.7.210:5000/commodityImage/${image}`" alt="商品图片" style="width: 100%; height: 100%;">
          </el-carousel-item>
        </el-carousel>
      </div>
      <h1 class="text item">{{ commodity.com_name }}</h1>
      <el-table :data="tableData" stripe style="width: 100%">
        <el-table-column prop="attr" label="属性" width="180" />
        <el-table-column prop="info" label="详情" width="180" />
      </el-table>


      <line-chart :data="commodity.com_prices"></line-chart>
     <br>
      <el-button type="primary" @click="addToCart">加入购物车</el-button>
      <el-button type="success" @click="buyNow">立即购买</el-button>
      <el-button type="info" @click="addToFavorites">收藏</el-button>
      <el-button type="info" @click="toStore">转到商家</el-button>

    </el-card>
  </div>

</el-main>
          <el-aside width="200px"></el-aside>  <!-- 添加了右边的aside -->
        </el-container>
        <el-footer>
          <div style="text-align: center;">Footer</div> <!-- 让Footer居中 -->
        </el-footer>
      </el-container>
    </div>







  
</template>

<script lang="ts">
import LineChart from './LineChart.vue'
import { defineComponent, onMounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import axios from 'axios'
interface Commodity {
  com_images: string[];
  com_prices: { com_pc_time: string; com_pc_price: number }[];
  comments: string[];
  com_id: number;
  com_name: string;
  com_introduction: string;
  com_oriPrice: number;
  com_expirationDate: string;
  com_uploadDate: string;
  com_left: number;
  com_rating: number;
  sto_name: string;
  sto_id: number;
  com_categories: string[];
}
interface TableData {
  attr: string;
  //info: string | number;
  info: string | undefined;

}


export default defineComponent({
  components: {
     'line-chart': LineChart,  // 假设你有一个名为 LineChartComponent 的组件
    // 'comments': CommentsComponent,  // 假设你有一个名为 CommentsComponent 的组件
  },
  setup() {
    const router = useRouter()
    const route = useRoute()
    const commodity = ref<Commodity | null>(null);
    const tableData = ref<TableData[]>([]);  // 创建一个响应式的 tableData 变量


    onMounted(async () => {
      try {
        const com_id = route.query.com_id

        console.log("已经接收到com_id="+com_id) 

        const response = await axios.get(
          //`http://localhost:5000/api/commodity/detail`
          `http://124.70.7.210:5000/api/commodity/detail`
          , {
          params: {
            com_id: com_id
          }
        });
        console.log("name："+response.data.com_name)        
        commodity.value = response.data;

        // 在这里初始化 tableData
        tableData.value = [
          {
            attr: '名称',
            info: commodity.value?.com_name,
          },
          {
            attr: '简介',
            info: commodity.value?.com_introduction,
          },
          {
            attr: '原始价格',
            info: commodity.value?.com_oriPrice.toString(),
          },
          {
            attr: '到期日期',
            info: commodity.value?.com_expirationDate,
          },
          {
            attr: '上传日期',
            info: commodity.value?.com_uploadDate,
          },
          {
            attr: '剩余量',
            info: commodity.value?.com_left.toString(),
          },
          {
            attr: '评分',
            info: commodity.value?.com_rating.toString(),
          },
          {
            attr: '商家名称',
            info: commodity.value?.sto_name,
          },
        ];
      } catch (error) {
        console.error(error);
      }
    });
 

    // const goToStore = () => {
    //   // 假设你有一个名为 storeDetail 的路由，你可以将商家ID作为参数传递
    //   router.push({ name: 'storeDetail', params: { store_id: commodity.value.store_id } })
    // }

    const addToCart = () => {
      // 这里你需要实现将商品添加到购物车的逻辑
    }

    const buyNow = () => {
      // 这里你需要实现立即购买商品的逻辑
    }

    const addToFavorites = () => {
      // 这里你需要实现将商品添加到收藏的逻辑
    }

    const toStore = () => {
      router.push({ path: '/storeDetail', query: { sto_id: commodity.value?.sto_id } })
      //ee
    }

    return {
      commodity,
      //呃呃，这一步也很重要
      tableData,
      //goToStore,
      addToCart,
      buyNow,
      addToFavorites,
      toStore,
    }
  }
})
</script>
