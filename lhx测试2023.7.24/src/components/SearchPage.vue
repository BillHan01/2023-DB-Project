<template>
    <el-row>
    <el-col :span="16" :offset="4">
    <el-card>
        <el-tabs 
            v-model="activeName"  
            @tab-click="handleClick"  
            type="card"
            @clear="handleClear"
        >
            <el-tab-pane label="商品" name="商品">
                <el-input v-model="search_str" placeholder="输入你想搜索的商品" clearable>
                    <template #append>
                        <el-button @click="onSearchCom"><el-icon><search/></el-icon> </el-button>
                    </template>
                </el-input>
            </el-tab-pane>
            <el-tab-pane label="商店" name="商店">
                <el-input v-model="search_str" placeholder="输入你想搜索的商店" clearable>
                    <template #append>
                        <el-button @click="onSearchSto"><el-icon><search/></el-icon> </el-button>
                    </template>
                </el-input>
            </el-tab-pane>
        </el-tabs>
    </el-card>
    <el-divider />
    <el-card v-show="List.length!=0">
        <Waterfall :list="List" v-bind="options" v-if="listIsCom">
            <template #item="{ item  }" >
                <div @click="handleClickList(item)" class="cardset">
                    <div style="padding: 2%;">
                        <LazyImg :url="item.com_firstImage" />
                    </div>
                    <div>
                        <el-row>
                            <el-col :span="16" :offset="2">
                                <h4 :style="{ color: item.com_color }">
                                {{ item.com_name }}
                                </h4>
                            </el-col>
                            <el-col :span="4" >
                                <h4>¥ {{ item.com_price }}</h4>
                            </el-col>
                        </el-row>
                    </div>
                </div>
            </template>
        </Waterfall>
        <Waterfall :list="List" v-bind="options" v-if="!listIsCom">
            <template #item="{ item  }" >
                <div @click="handleClickList(item)" class="cardset">
                    <div style="padding: 2%;">
                        <LazyImg :url="item.sto_firstImage" />
                    </div>
                    <div>
                        <el-row>
                            <el-col :span="16" :offset="2">
                                <h4>
                                {{ item.sto_name }}
                                </h4>
                            </el-col>
                        </el-row>
                        <el-row>
                            <el-col :span="16" :offset="2">
                            <el-tag v-for="(tag, index) in item.com_categories" :key="index">{{ tag }}</el-tag>
                             </el-col>
                        </el-row>
                        <br />
                    </div>
                </div>
            </template>
        </Waterfall>
    </el-card>
    <el-card v-show="List.length==0">
       <h1 class="bg-gray-900">无搜索结果</h1>
    </el-card>
    </el-col>
    </el-row>
</template>

<script setup lang="ts">
import { TabsPaneContext } from 'element-plus/es/components/tabs/src/constants';
import { ref, reactive, onBeforeMount} from 'vue';
import { LazyImg, Waterfall } from "vue-waterfall-plugin-next";
import error from "../assets/error.png";
import loading from "../assets/loading.png";
import "vue-waterfall-plugin-next/dist/style.css";
import axios from 'axios';
import {  useRouter } from 'vue-router'
const router = useRouter()
function handleClickList(item){
   
    if(listIsCom.value){
        //点击商品
        console.log(item)
        router.push({ 
            path: '/commodityDetail', 
            query: { com_id: item.com_id } 
        })
    }
    else{
        //点击商家
        console.log(item)
        router.push({ 
            path: '/storeDetail', 
            query: { sto_id: item.sto_id } 
        })
    }
}
const options = reactive({
     // 是否有周围的gutter
    hasAroundGutter: true,
    gutter:10,
    loadProps: {
        loading,
        error
    },
    lazyload: true
})
const listIsCom=ref(true);

const List=reactive(Array<object>() );

const activeName=ref("商品");
const search_str=ref("");
const handleClick = (tab: TabsPaneContext, event: Event) => {
  console.log(activeName.value);
    //切换标签
}
function onSearchCom(){
    console.log("搜索内容为：",search_str.value);
    if(search_str.value.length!=0){
        listIsCom.value=true;
        axios.post(
            //'http://localhost:5000/api/search/commodityList',
            'http://124.70.7.210:5000/api/search/commodityList',
              JSON.stringify({
                search_str: search_str.value,
                sort_order: 0
              }), {
            headers: {
                'Content-Type': 'application/json'
            }
          }
        ).then(response => {
            if("msg" in response.data){
                console.log(response.data.msg)
                List.splice(0,List.length);
            }
            else if( response.data.com_list.length==0){
                console.log('结果为空')
                List.splice(0,List.length);
            }
            else{
                console.log('列表为',response.data)
                var len=List.length;
                List.splice(0,len+1, ...response.data.com_list)
                //处理商品颜色
                List.forEach((item)=>{
                    var expirationDate = new Date(item.com_expirationDate.replace(/-/g,"/"));
                    var dateNow = new Date();
                    var dateDiff = expirationDate.getTime() - dateNow.getTime();//时间差的毫秒数
                    var dayDiff = Math.floor(dateDiff / (24 * 3600 * 1000));//计算出相差天数，向下取整
                    if(dayDiff<1)
                        item.com_color="red";
                    else if(dayDiff<3)
                        item.com_color="yellow";
                    else    
                        item.com_color="green";
                    
                    item.com_firstImage=
                    //"http://localhost:5000/commodityImage/"
                    "http://124.70.7.210:5000/commodityImage/"
                    +item.com_firstImage;
                })
            }
        }).catch(error => {
                console.error('搜索失败', error);
        });
    }
}
function onSearchSto(){
    console.log("搜索内容为：",search_str.value);
    if(search_str.value.length!=0){
        listIsCom.value=false;
        axios.post(
            //'http://localhost:5000/api/search/storeList',
            'http://124.70.7.210:5000/api/search/storeList',
              JSON.stringify({
                search_str: search_str.value,
              }), {
            headers: {
                'Content-Type': 'application/json'
            }
          }
        ).then(response => {
            if("msg" in response.data){
                console.log(response.data.msg)
                List.splice(0,List.length);
            }
            else if( response.data.sto_list.length==0){
                console.log('结果为空')
                List.splice(0,List.length);
            }
            else{
                console.log('列表为',response.data)
                var len=List.length;
                List.splice(0,len+1, ...response.data.sto_list)

                List.forEach((item)=>{
                    item.sto_firstImage=
                        //"http://localhost:5000/storeImage/"
                        "http://124.70.7.210:5000/storeImage/"
                        +item.sto_firstImage;
                    item.com_list.forEach((item)=>{
                        item.com_firstImage=
                       //"http://localhost:5000/commodityImage/"
                        "http://124.70.7.210:5000/commodityImage/"
                        +item.com_firstImage;
                    })
                })
            }
        })
        .catch(error => {
                console.error('搜索失败', error);
        });
    }
}
function handleClear(){
    search_str.value="";
    console.log(search_str.value);
}
</script>
<style>
.cardset{
    background-color: #c7edf5;
    border-radius:4%

}
</style>