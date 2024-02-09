import { createSlice } from "@reduxjs/toolkit";
import { createSelector } from "reselect";
import { apiCallBegan } from "./api";
import moment from "moment";

const slice = createSlice({
  name: "articles",
  initialState: {
    list: [],
    loading: false,
    lastFetch: null
  },
  reducers: {
    articlesRequested: (articles, action) => {
      articles.loading = true;
    },

    articlesReceived: (articles, action) => {
      articles.list = action.payload.data;
      articles.loading = false;
      articles.lastFetch = Date.now();
    },

    articlesRequestFailed: (articles, action) => {
      articles.loading = false;
    },

    articleAdded: (articles, action) => {
      console.log("Add payload", action.payload);
      articles.list.push(action.payload.data);
    },

    articleEdited: (articles, action) => {
      console.log("Edit payload", action.payload);
      const index = articles.list.findIndex(
        (article) => article.id === action.payload.data.id
      );
      articles.list[index] = action.payload.data;
    },

    articleDeleted: (articles, action) => {
      console.log("action.payload", action.payload);
      return {
        ...articles,
        list: articles.list.filter(
          (article) =>
            article && article.id !== parseInt(action.payload.message)
        )
      };
    }
  }
});

export const {
  articleAdded,
  articleDeleted,
  articleEdited,
  articlesReceived,
  articlesRequested,
  articlesRequestFailed
} = slice.actions;
export default slice.reducer;

// Action Creators
const url = "/article";
const token = localStorage.getItem("accessToken");
export const loadArticles = () => (dispatch, getState) => {
  const { lastFetch } = getState().articles;

  const diffInMinutes = moment().diff(moment(lastFetch), "minutes");
  if (diffInMinutes < 1) return;

  return dispatch(
    apiCallBegan({
      url,
      onStart: articlesRequested.type,
      onSuccess: articlesReceived.type,
      onError: articlesRequestFailed.type,
      token
    })
  );
};

export const addArticle = (article) =>
  apiCallBegan({
    url,
    method: "post",
    data: { Value: article },
    onSuccess: articleAdded.type,
    token
  });

export const editArticle = (article) =>
  apiCallBegan({
    url,
    method: "put",
    data: { Value: article },
    onSuccess: articleEdited.type,
    token
  });

export const deleteArticle = (articleId) =>
  apiCallBegan({
    url: url + "/" + articleId,
    method: "delete",
    data: articleId,
    onSuccess: articleDeleted.type,
    token
  });

// Selector
// Memoization

export const getArticlesByUser = (userId) =>
  createSelector(
    (state) => state.entities.articles,
    (articles) => articles.filter((article) => article.userId === userId)
  );

export const selectArticles = createSelector(
  (state) => state.articles,
  (articles) => articles.list
);

export const selectArticlesLoading = createSelector(
  (state) => state.articles,
  (articles) => articles.loading
);

export const selectArticle = (articleId) => {
  console.log("article id", articleId);
  const selector = createSelector(
    (state) => state.articles,
    (articles) =>
      articles.list.find((article) => article.id === Number(articleId))
  );
  return (state) => {
    const selectedArticle = selector(state);
    console.log("Selected article", selectedArticle);
    return selectedArticle;
  };
};

// export const selectArticleByarticleId = (articleId) => {
//   console.log("article id", articleId);
//   const selector = createSelector(
//     (state) => state.articles,
//     (articles) =>
//       articles.list.find((article) => article.id === Number(articleId))
//         .articles
//   );
//   return (state) => {
//     const selectdArticle = selector(state);
//     console.log("Selected article", selectdArticle);
//     return selectdArticle;
//   };
// };
