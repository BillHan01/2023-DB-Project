

<template>
    <div class="common-layout">
    <el-container>
        <el-header>
          <div style="text-align: center;">商家详情页面</div>
        </el-header>
        <el-container>
          <el-aside width="200px"></el-aside>
          <el-main>

  <div v-if="store">
      <el-card class="box-card">
        <el-row>
            <el-col :span="12">
              <el-input v-model="searchQuery" placeholder="请输入商品名称"></el-input>
            </el-col>
            <el-col :span="12">
              <el-button type="primary" @click="searchProducts">店内搜索</el-button>
            </el-col>
          </el-row>    
          <br>      
              <!-- <img :src="store.sto_licenseImg" alt="店面图" class="store-img"> -->

              <div style="width: 400px" >
                <el-carousel height="200px" width="100px">
                  <el-carousel-item v-for="(image, index) in store.sto_imageList" :key="index">
                    <!-- <img 
                    :src="`http://localhost:5000/storeImage/${image}`" 
                    alt="商品图片" style="width: 100%; height: 100%;"> -->
                    <img 
                    :src="`http://124.70.7.210:5000/storeImage/${image}`" 
                    alt="商品图片" style="width: 100%; height: 100%;">
                  </el-carousel-item>
                </el-carousel>
              </div>

              <div class="store-details">

                  <h2>{{ store.sto_name }}</h2>
                  <p>{{ store.sto_introduction }}</p>
                  <p><strong>地址：</strong>{{ store.user_address }}</p>
                  <h3><strong>主营类别：</strong></h3>
                  <el-tag v-for="(category, index) in store.com_categories" :key="index">
                      {{ category }}
                  </el-tag>
              </div>

          <el-divider></el-divider>
          <h3><strong>当前公告：</strong></h3>
          <el-timeline>
              <el-timeline-item v-for="(notice, index) in store.sto_notices" :key="index">
                  {{ notice }}
              </el-timeline-item>
          </el-timeline>
          <el-divider></el-divider> 
          <el-button type="primary" @click="goToCommodity">所有商品</el-button>
          <el-button type="primary"  @click="dialogVisible = true">
            查看证书
          </el-button>
          <!-- <el-button type="primary" @click="goToCertificate">查看证书</el-button> -->
          <!-- 新增的对话框代码 -->
          <el-dialog
            v-model="dialogVisible"
            title="经营许可证"
            width="50%"
            :before-close="handleClose"
          >
            <!-- <img :src="`http://localhost:5000/storeLicenseImg/${store.sto_licenseImg}`" alt="经营许可证" style="width: 100%;"> -->
            <img :src="`http://124.70.7.210:5000/storeLicenseImg/${store.sto_licenseImg}`" alt="经营许可证" style="width: 100%;">
            <template #footer>
              <span class="dialog-footer">
                <el-button @click="dialogVisible = false">Cancel</el-button>
                <el-button type="primary" @click="dialogVisible = false">
                  Confirm
                </el-button>
              </span>
            </template>
          </el-dialog>
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
import { defineComponent, onMounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import axios from 'axios'
import { ElMessageBox } from 'element-plus'

interface Store {
  sto_licenseImg: string;
  sto_imageList: string[];
  sto_notices: string[];
  sto_id: number;
  sto_name: string;
  sto_introduction: string;
  com_categories: string[];
  user_address: string;
}

export default defineComponent({
setup() {
  const router = useRouter()
  const route = useRoute()
  const store = ref<Store | null>(null);
  const searchQuery = ref('');  // 新增的搜索查询变量
  //const showDialog = ref(false);  // 新增的对话框显示控制变量
  const dialogVisible = ref(false)

  onMounted(async () => {
    try {
      const sto_id = route.query.sto_id

      const response = await axios.get(
        //`http://localhost:5000/api/store/detail`
        `http://124.70.7.210:5000/api/store/detail`
        , {
        params: {
          sto_id: sto_id
        }
      });

      store.value = response.data;
    } catch (error) {
      console.error(error);
    }
  });

  const goToCommodity = () => {
    // 这里你需要实现跳转到商品搜索页面的逻辑
  }
  const handleClose = (done: () => void) => {
      ElMessageBox.confirm('Are you sure to close this dialog?')
        .then(() => {
          done()
        })
        .catch(() => {
          // catch error
        })
    }
  // const goToCertificate = () => {  // 修改的查看证书方法
  //   console.log("开始显示")
  //   showDialog.value = true;  // 显示对话框
  //   console.log("结束显示")
  // }
  const searchProducts = async () => {  // 新增的搜索商品方法
    // 这里你需要实现根据 searchQuery 的值来搜索商品的逻辑
  }


  return {
    store,
    goToCommodity,
    searchQuery,  // 新增的搜索查询变量
    searchProducts,  // 新增的搜索商品方法
    //goToCertificate,  // 修改的查看证书方法
    //showDialog,
    handleClose,
    dialogVisible,

  }
}
})
</script>

<style scoped>
.store-header {
  display: flex;
  align-items: center;
}
.store-carousel {
  display: flex;
  justify-content: center;
  margin-bottom: 20px;
}

.store-img {
  width: 400px;
  height: 400px;
  margin-right: 20px;
}

.store-details h2 {
  margin-bottom: 0.5em;
}

.store-details h3 {
  margin-top: 1em;
  margin-bottom: 0.5em;
}
</style>
