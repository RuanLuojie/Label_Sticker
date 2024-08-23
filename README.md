### README.md

```markdown
# Sticky Note

Sticky Note 是一個簡單的 WPF 應用程式，用於顯示和編輯筆記。它支援文本顯示、編輯功能，並提供一個簡單的自訂標題欄。

## 功能

- 顯示和編輯筆記。
- 支援筆記內容的儲存和重新載入。
- 自訂標題欄，提供關閉、最小化和最大化按鈕。
- 支援拖動和調整視窗大小。

## 安裝

1. 將此儲存庫複製到你的本地機器：

   ```bash
   git clone https://github.com/yourusername/StickyNote.git
   ```

2. 打開 Visual Studio 並載入解決方案文件 `Label_Sticker.sln`。

3. 確保你已安裝 .NET Framework 4.7.2 或更高版本。

4. 編譯並執行應用程式。

## 使用

1. 啟動應用程式後，將顯示一個主要視窗，包含筆記內容。
2. 使用編輯按鈕 (`✎`) 打開編輯模式，編輯筆記內容。
3. 編輯完成後，儲存筆記內容，視窗將顯示更新後的筆記。

## 文件結構

- `MainWindow.xaml`：定義應用程式的主視窗佈局和樣式。
- `MainWindow.xaml.cs`：主視窗的程式碼後端邏輯，處理事件和功能實現。
- `EditWindow.xaml`：定義編輯筆記的視窗佈局。
- `EditWindow.xaml.cs`：編輯視窗的程式碼後端邏輯，處理筆記的編輯和儲存。
- `Work_itme.txt`：儲存筆記內容的文本文件。

## 常見問題

### 1. 如何調整視窗大小？

視窗可以通過拖動邊緣來調整大小，這是通過在 `MainWindow.xaml` 中設定 `ResizeMode="CanResizeWithGrip"` 屬性來實現的。

### 2. 如何儲存筆記內容？

點擊編輯按鈕進入編輯模式，修改筆記內容後點擊儲存按鈕，內容將儲存到 `Work_itme.txt` 文件中。

## 貢獻

歡迎任何形式的貢獻！請提交問題、功能請求或拉取請求。

## 授權

這個專案採用 MIT 許可證 - 請參閱 [LICENSE](LICENSE) 文件以獲取更多詳細信息。
```

### 說明

1. **概述**：使用簡單的語言來描述應用程式的功能和用途。
2. **安裝指南**：提供詳細的安裝步驟，指導用戶如何設置開發環境。
3. **使用方法**：解釋如何使用應用程式的基本功能和操作。
4. **文件結構**：列出主要文件及其功能，幫助開發者理解程式碼結構。
5. **常見問題**：提供常見問題的解答，幫助用戶排除故障。
6. **貢獻**：邀請開發者參與項目，說明如何貢獻。
7. **授權**：提供有關專案使用許可的資訊。

