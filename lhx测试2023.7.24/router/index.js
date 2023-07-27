import {createRouter,createWebHistory} from 'vue-router'
import HomePage from '../src/components/TheWelcome.vue';
import SearchPage from '../src/components/SearchPage.vue';
import CommodityDetailPage from '../src/components/CommodityDetailPage.vue';
import storeDetaillPage from '../src/components/storeDetailPage.vue';
const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/welcome',
            name:'welcome',
            component: HomePage
        },
        {
            path: '/search',
            name:'search',
            component: SearchPage
        },
        {
            path: '/commodityDetail',
            name: 'commodityDetail',
            component: CommodityDetailPage,
        },
        {
            path:'/storeDetail',
            name:'storeDetail',
            component: storeDetaillPage
        }
    ]
});

export default router;